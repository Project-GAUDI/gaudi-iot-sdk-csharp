using System;

namespace Microsoft.Azure.Devices.Client.Transport.Util
{
    /// <summary>
    /// Get max message size class.
    /// </summary>
    public class MessageSize
    {
        private const string envMessageSizeLimitExpansion = "MessageSizeLimitExpansion";
        private const int maxMessageSize = 16 * 1024 * 1024;

        /// <summary>
        /// Get max message size.
        /// </summary>
        /// <param name="defaultMaxSize"></param>
        public static int getMaxMessageSize(int defaultMaxSize)
        {
            // デフォルト値設定
            int retMaxMessageSize = defaultMaxSize;
            string strMessageSizeLimitExpansion = Environment.GetEnvironmentVariable(envMessageSizeLimitExpansion);

            if (false == string.IsNullOrEmpty(strMessageSizeLimitExpansion))
            {
                if (bool.TryParse(strMessageSizeLimitExpansion, out var limitExpansion))
                {
                    if (limitExpansion)
                    {
                        // MessageSizeLimitExpansion=trueの場合は大容量用上限を設定
                        retMaxMessageSize = maxMessageSize;
                    }
                }
            }

            return retMaxMessageSize;
        }
    }
}