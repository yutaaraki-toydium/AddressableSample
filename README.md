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
<img width="363" alt="image" src="https://user-images.githubusercontent.com/55943829/190857805-efc40c0d-b24c-4dd6-9c49-c2a795182701.png">

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
<img width="363" alt="image" src="https://user-images.githubusercontent.com/55943829/190858409-4f0fd132-c5e1-44b1-a096-00f218192b6e.png">

* catalogは素材の依存関係情報と、その変更チェック用のファイル
* .bundleは素材その物を圧縮したファイル(いわゆるアセットバンドル）

## S3等にこれらのファイルをアップロード
* 簡易的にServerData/xxx以下をLocalでホストすれば簡単なテストは行なえます。

## Profileの設定
* UnityでWindow>Asset Management>Addressables>Profilesを選択してAddressables Profilesを開く
* RemoteLoadPathを先にアップロードしたURLに変更

※下記ではLocalHostの:8000でServerData/StandaloneOSXをホストしているのでhttp://localhost:8000と設定しています。

※S3やCDNのアドレスを入れるプロファイルを別途作れば切り替えは簡単になるかと思います。
<img width="363" alt="image" src="https://user-images.githubusercontent.com/55943829/190858539-4cd4a161-d54d-4d41-9297-54d5a264ad7d.png">

Addressables GroupsのPlay Mode ScriptをUse Existing Buildに変更します。Use Existing Buildにすることでアセットバンドルから素材を読み込むようになります。


## 以下留意点

### カタログの肥大化には注意する
各AssetBundleへのリンクや内容物へのIndexが保管されているのがカタログですが、このカタログが肥大化するとパフォーマンスが低下しやすいです。
* Jsonデータなので件数が増えるとパースするだけで結構重くなる
* メモリも食う
* 一定のしきい値以上のアイテムが登録された場合は新しいGroupを作るような仕組みは必要
* どうしても1カタログで管理したい場合はJsonをProtobufの様なパース速度が高速なフォーマットなどに変更するのが良い
  * メンテナビリティ等を考慮すると完全に悪手なので、おすすめはしない
  * Addressables/AssetBundleの設計思想的に細かいファイルを大量に保存するようには出来ていない。
  * これは細かい実装を追っていけば分かりやすい
  * 精々Prefab単位でデータを登録し、カテゴリ毎くらいにラベル分けし、ラベル＝AssetBundleというような形が望ましい
  * AssetBundleの仕組み上ファイルの利用状況をファイルシステム上にLockファイルを生成して管理しているので、そのロックファイル＋開いてるAssetBundleがOSのしきい値を超えてしまうとそこでエラーになってしまう。

### Group管理やラベル生成用のEditorスクリプト
Groupの作成やラベルの生成にはそれ用のEditorスクリプトを用意すること
* 基本的に多数のアセットをDrag&Dropで登録していく管理方法は破綻しやすいです
* 登録件数が多いと登録処理に時間がかかったりします
* ディレクトリ構造からGroup分けやラベル生成を作れる様にするのがアセットの指定と合わせやすく、直感的に使えて楽だと思います。
* ビルド時にこのスクリプトを実行すれば漏れ等も減らせます

### デプロイフロー
* 最終的には上記の`Build>New Build>Default Build Scriptでアセットバンドルをビルド`相当の動きをするEditor Scriptを作る
* それを実行+成果物をS3へアップロードする、というスクリプトでAssetBundleのデプロイをする形
* S3に関してはアップロード先を決めておきCloudFrontをその前に立てて上げて最終的なURLが確定出来ればそれをUnity内でセットする形
* 上記で`localhost`への接続を設定している箇所をCDNのURLに置き換えればすぐ参照先をCDNにしたビルドを作れるはず
