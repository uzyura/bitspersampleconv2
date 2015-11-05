# 説明 #

SqWave2は矩形波の入ったWAVファイルを出力するツールです。

WaveGene 1.40の矩形波出力波形に納得いかない感じだったので作りました。

[矩形波出力　性能比較](SquareWaveOutput.md)

# 使用方法 #

http://bitspersampleconv2.googlecode.com/files/SqWave10011.zip
をダウンロードして、中に入っているsetup.exeを実行するとインストールされます。

デスクトップにSqWave2という名前のショートカットが作られますのでこれをダブルクリックします。あとは画面の指示に従ってWAVファイルを出力します。

アンインストールは、コントロールパネルの[プログラムと機能]からSqWave2を選んでアンインストールを選択するとできます。

![http://bitspersampleconv2.googlecode.com/files/SqWave10008ss.png](http://bitspersampleconv2.googlecode.com/files/SqWave10008ss.png)

CTRLキーを押しながら出力チャンネルを選択すると
出力チャンネルを複数選択できます。

## ASIO出力動作確認デバイス ##

Windows7 64ビット版で動作確認。

ASIO出力モードは、SqWave2バージョン1.11でそこそこ安定動作するようになりました。

以下のASIOデバイスでは動作しました。
  * M-AUDIO ProFire 2626
  * Creative X-Fi Ti HD (ただしサンプリングレート192kHzに対応していないので、SqWave2起動後、左の欄で96000Hzにセットしてからデバドラを初期化する必要あり)
  * RME FIREFACE 400 …ドライババージョン3.016ですべて正常動作。古いドライババージョン2.9992を使用すると、サンプリングレートを96kHzに設定すると正常動作しますが、192kHzに設定すると、間欠的に音が鳴ります。ちなみにFirefaceのデバドラは、バージョン2．9992時点で、MMEで音を鳴らしている最中にASIOドライバでサンプルレート設定を変更すると、ブルースクリーン(APC\_INDEX\_MISMATCH)が発生してWindowsが再起動します。

以下のASIOデバイスでは正常動作しませんでした。私のバグが原因かもしれないので製品のせいではないかもしれません
  * ASIO4ALL …なぜか間欠的に音が鳴る。しかしそもそも、ASIO4ALLって、意味あるのでしょうか。積極的に対応する気になりません
  * Prodigy HD2 …ドライババージョン1.08。音はM-AUDIO並みにちゃんと鳴りますが、やたら不安定です。Foobar2000でも不安定になるのでProdigyのデバドラの問題か？
  * Realtek ALC889 …Intel DX58SO Realtek ALC Audio Driver 5964 11/2/2009で機能は正常動作するが、音が明らかに悪いです(プラシーボとかのレベルではなく、明らかに関係ない周波数成分が鳴っている、歪が多い感じ。)

[ASIOデバイスについて](ASIO.md)

## 動作確認OS ##
  * Windows XP Professional SP3 32ビット版
  * Windows 7 Professional 64ビット版

# 開発方法 #

このプログラムは.NET Framework 4 C#(と少々のネイティブC++)を使用して作りました。

Visual Studio 2010が必要です。

TortoiseSVNなどを使って https://bitspersampleconv2.googlecode.com/svn/trunk をチェックアウトします

チェックアウトしてきたツリーにASIO SDKをunzipしたものをコピーして 同じ親ディレクトリのなかにASIOSDK2とAsioIOがある状態にして AsioIO\AsioIODLL\copyasiofiles.batを実行するとASIOSDKの中から asio.cppなど9つのファイルをAsioIO\AsioIODLL\にコピーします

Visual Studio 2010でsqwave2.slnを開いてF5でビルドして実行します

# 更新履歴 #

### SqWave2 バージョン 1.0.11 ###
  * 出力チャンネル設定がひどくバグっているのを修正。

### SqWave2 バージョン 1.0.10 ###
  * ASIOのCreateBuffersを、使用ポートだけ確保するように修正。
  * RME Fireface 400が普通に鳴るようになった。

### SqWave2 バージョン 1.0.9 ###
  * ASIO出力バッファサイズをpreferredからmaxに増やす。
  * 2回目以降のASIOStartで前回の最後の出力が1バッファ分繰り返される問題を修正。
  * M-AUDIO ProFire 2626のASIO出力が納得のいく出来になった。
  * ASIO4ALLで鳴るかどうかはASIO4ALLの先につながっているオーディオデバイスによるようだ
  * RealTek ASIOはまだ変な音が鳴る

### SqWave2 バージョン 1.0.8 ###
  * ASIO出力対応。

### SqWave2 バージョン 1.0.4 ###
  * 級数加算打ち切り周波数をナイキスト周波数との比の％で入力できるようにしました。

### SqWave2 バージョン 1.0.2 ###
  * のこぎり波、三角波もでるようにしました。

### SqWave2 バージョン 1.0.1 ###
  * ダイアログに入れられた値の範囲チェック。
  * マルチコア対応。Intel Core i7-980Xでの実行結果。見事にすべてのCPUコアに負荷が分散していますｗ (矩形波、10Hz、30秒のWAVファイル生成中の様子)

![http://bitspersampleconv2.googlecode.com/files/taskman.png](http://bitspersampleconv2.googlecode.com/files/taskman.png)

### SqWave2 バージョン 1.0.0 ###
  * 新規作成

### SqWave1 バージョン 1.0.0 ###
  * 新規作成。
  * 誤ってプロジェクトファイルを壊してしまったため廃棄


---


ASIO is a trademark of Steinberg Media Technologies GmbH.