# WASAPI排他モードでWAVファイルを録音 #

RecPcmWinはWASAPI排他モードのイベント駆動モードでWAVファイルを録音するプログラムです。

Windows 7に対応しております。

**RecPcmWinは実験プログラムです。録音中にメモリ不足エラーが発生し、録音データが保存できないことがあります。確実に録音したい場合は、他のソフトを使ってください。**

~~ビットマッチ録音するために作成したソフトウェアのため、最大音量で録音します。~~録音音量は、Windowsミキサーのセッションボリュームの設定値になるようです。

# ダウンロード #

  * 64ビット版 http://bitspersampleconv2.googlecode.com/files/RecPcmWin104x64.zip

# 使用方法 #

zipを展開して、中のSetup.exeを実行してインストールし、
RecPcmWinを開き、再生するWAVファイルを指定して、
使用する出力デバイスを選択し、再生ボタンを押します。

アンインストールは、プログラムの追加と削除で行うか、もう一度Setup.exeを実行して削除を選びます。

![http://bitspersampleconv2.googlecode.com/files/RecPcmWin101.jpg](http://bitspersampleconv2.googlecode.com/files/RecPcmWin101.jpg)

# ソースコード #

閲覧だけなら:
  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/RecPcmWin
  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/WasapiCS
  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/WasapiIODLL
  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/WavRWLib2

WASAPIの関数を呼び出しているところは、WasapiUser.cppにあります。
http://code.google.com/p/bitspersampleconv2/source/browse/trunk/WasapiIODLL/WasapiUser.cpp


ビルドしたい場合は、Sourceページに行ってソースコードをTortoiseSVNなどでチェックアウトしてきます。
RecPcmWin/RecPcmWin.slnをVS2010で開いてビルド。

# 参考文献 #

http://msdn.microsoft.com/en-us/library/dd370800%28v=VS.85%29.aspx

# 更新履歴 #
### RecPcmWin 1.0.4 ###
  * 量子化ビット数24ビットの録音に対応。

### RecPcmWin 1.0.1 ###
  * 64ビット版のインストーラーに、WasapiIODLL.DLLが入っていなかったのを修正。

### RecPcmWin 1.0.0 ###
  * 新規作成。
  * 32ビット版でバッファを800MBにすると、録音開始直後にメモリ不足になる問題あり。