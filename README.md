# gaudi-iot-sdk-csharp

## 目次

* [概要](#概要)
* [Quick Start](#quick-start)
* [カスタムパッケージ一覧](#カスタムパッケージ一覧)
* [Deployment 設定値](#deployment-設定値)
  * [環境変数](#環境変数)
* [Feedback](#feedback)
* [LICENSE](#license)

## 概要
gaudi-iot-sdk-csharpは、azure-iot-sdk-csharpをベースに一部カスタムを行ったSDKパッケージを含むパッケージです。
gaudi-iot-sdk-csharpはAzure IoT Hubサービスに接続および管理されるアプリケーションの構築を容易にするコードを含む統括パッケージです。

## Quick Start


## カスタムパッケージ一覧

| カスタムパッケージ名                 | 概要                                                         | 元パッケージからの追加・修正点                                  | 元パッケージ名                                   | 元バージョン |
| ---------------------------------- | ------------------------------------------------------------ | ------------------------------------------------------------  | ----------------------------------------------- | ------- |
| Gaudi.Azure.Devices.Client         | IoTHubとのメッセージ送受信、ツインの同期、ダイレクトメソッドの実装 | メッセージサイズ上限を16MBに拡張できるよう環境変数追加 | Microsoft.Azure.Devices.Client                  | 1.42.3 |



## Deployment 設定値

### 環境変数

#### 環境変数の値

| Key                       | Required | Default | Recommend | Description                                                     |
| ------------------------- | -------- | ------- | --------- | ---------------------------------------------------------------- |
| MessageSizeLimitExpansion |          | false   |           | モジュール間のメッセージサイズの上限拡張指定。<br>["true", "false"]<br>true：メッセージサイズ上限が拡張され16MBになる(※1,2)。<br>false：メッセージサイズ上限はデフォルトの256KBになる。 |

※1：**IoTHubへ**のupstreamする際のメッセージサイズ上限は256KBのまま変わらない。<br>
送信してしまった場合、edgeHubからの送信がエラーとなり、その後のメッセージ送信も停滞してしまう。<br>
Edge→Fog間の拡張サイズメッセージの送信は可能。<br>
※2：edgeHubは、メッセージサイズ拡張対応が入っているGAUDIIotEdge-Hubを使用し、edgeHub・送信先モジュールにもMessageSizeLimitExpansion=trueが設定されている必要がある。<br>


## Feedback
お気づきの点があれば、ぜひIssueにてお知らせください。

## LICENSE
gaudi-iot-sdk-csharp is licensed under the MIT License, see the [LICENSE](LICENSE) file for details.