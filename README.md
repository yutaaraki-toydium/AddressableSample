# AddressableSample


## 導入
Package Manager
* Packages: Unity Registry
* Addressablesを検索Install

インストールが完了したら
* Window>Asset Management>Addressables>Groups
* Create Addressables Settings

### Groupの設定
Addressables Group設定を開く
* New > PackedAssetsでグループ作成
* 適当な名前を付ける
*　データを登録する
画像ではLuffy/Zoro/BuggyのPng/Prefabを登録しています
<img width="719" alt="image" src="https://user-images.githubusercontent.com/55943829/190857805-efc40c0d-b24c-4dd6-9c49-c2a795182701.png">

Groupの設定をRemoteにする
* Groupを選択（画像ではPackage 001）
* Build & Load PathをRemoteに設定する
![貼り付けた画像_2022_09_17_21_55](https://user-images.githubusercontent.com/55943829/190857941-68c60786-69a0-42e2-9389-557151cf4ba1.png)

### AddressableAssetSettings

Window>Asset Management>Addressables>SettingsからAddressable Asset Settingsを開きます。

* Player Version Override に管理しやすい文字列を入れます。ここでは「1.0」
* Build Remote Catalogをチェックします。
* Build & Load PathをRemoteに設定
![貼り付けた画像_2022_09_17_22_01](https://user-images.githubusercontent.com/55943829/190858251-a1aa7687-aa4e-407c-9e8a-bc6aff74617a.png)

### Dataのビルド
Addressables GroupsウインドウでBuild>New Build>Default Build Scriptでアセットバンドルをビルド
<img width="363" alt="image" src="https://user-images.githubusercontent.com/55943829/190858339-28c14b7a-e3ff-4292-8850-932df51e1dd3.png">


問題なければプロジェクトファイルの中にServerDataフォルダが作成され、下記のようなフォルダー構成ができている
<img width="465" alt="image" src="https://user-images.githubusercontent.com/55943829/190858409-4f0fd132-c5e1-44b1-a096-00f218192b6e.png">

* catalogは素材の依存関係情報と、その変更チェック用のファイル
* .bundleは素材その物を圧縮したファイル(いわゆるアセットバンドル）

## S3等にこれらのファイルをアップロード
* 簡易的にServerData/xxx以下をLocalでホストすれば簡単なテストは行なえます。

## Profileの設定
* UnityでWindow>Asset Management>Addressables>Profilesを選択してAddressables Profilesを開く
* RemoteLoadPathを先にアップロードしたURLに変更

※下記ではLocalHostの:8000でServerData/StandaloneOSXをホストしているのでhttp://localhost:8000と設定しています。

※S3やCDNのアドレスを入れるプロファイルを別途作れば切り替えは簡単になるかと思います。
<img width="754" alt="image" src="https://user-images.githubusercontent.com/55943829/190858539-4cd4a161-d54d-4d41-9297-54d5a264ad7d.png">


* Build & Load PathをRemoteに設定
