// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Devices.Client.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;
    using DotNetty.Transport.Channels;

    public sealed class ReadListeningHandler : ChannelHandlerAdapter
    {
        readonly Queue<object> receivedQueue = new Queue<object>();
        TaskCompletionSource<object> readPromise;
        Exception registeredException;

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            TaskCompletionSource<object> promise = this.readPromise;
            if (this.readPromise != null)
            {
                this.readPromise = null;
                promise.TrySetResult(message);
            }
            else
            {
                this.receivedQueue.Enqueue(message);
            }
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            this.SetException(new InvalidOperationException("Channel is closed."));
            base.ChannelInactive(context);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception) => this.SetException(exception);

        void SetException(Exception exception)
        {
            this.registeredException = exception;
            this.readPromise?.TrySetException(exception);
        }

        public async Task<object> ReceiveAsync(TimeSpan timeout = default(TimeSpan))
        {
            Contract.Assert(this.readPromise == null);

            if (this.registeredException != null)
            {
                throw this.registeredException;
            }

            if (this.receivedQueue.Count > 0)
            {
                return this.receivedQueue.Dequeue();
            }

            var promise = new TaskCompletionSource<object>();
            this.readPromise = promise;

            if (timeout > TimeSpan.Zero)
            {
                Task task = await Task.WhenAny(promise.Task, Task.Delay(timeout)).ConfigureAwait(false);
                if (task != promise.Task)
                {
                    throw new TimeoutException("ReceiveAsync timed out");
                }

                return promise.Task.Result;
            }

            return await promise.Task.ConfigureAwait(false);
        }
    }
}