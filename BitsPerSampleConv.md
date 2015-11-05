BPSConvWinは16bit PCM WAVファイルを読み込み、
音量を変えずに量子化ビット数を減らし、別ファイルに保存するWindows用アプリケーションです。

![http://bitspersampleconv2.googlecode.com/files/bpsconvwin113.jpg](http://bitspersampleconv2.googlecode.com/files/bpsconvwin113.jpg)

# ダウンロード #

http://bitspersampleconv2.googlecode.com/files/BpsConvWin118.zip

# 使い方 #

16bit PCM WAVファイルをCDからripするなどして用意します。

BpsConvWin.exeをダブルクリックして実行してください。
読み出したWAVファイルと同じフォルダに
量子化ビット数が1～15ビットに減らされた15個のWAVファイルが作られます。

読み込むWAVファイル自体は読み出しに使用するだけで、書き換えません。

出力ファイルを書き込むときに同名のファイルがすでに存在した場合、エラー終了します。
これは、すでに存在するファイルを上書きしないようにするためです。

# 出てくるWAVファイルの音について #

|量子化ビット数|ダイナミックレンジ|
|:------|:--------|
|16ビット(CDの音質)|96dB     |
|15ビット  |90dB     |
|14ビット(初期の一部のCDプレーヤー)|84dB     |
|13ビット  |78dB     |
|12ビット  |72dB     |
|11ビット  |66dB     |
|10ビット  |60dB     |
|9ビット   |54dB     |
|8ビット(固定電話)|48dB     |

**量子化ビット数1ビットのWAVファイルはとんでもない大音量の雑音が入ります。再生音量に注意してください。**

# ノイズシェイピング機能 #

このノイズシェイピングは最も簡単な1次バターワース型です

# ディザを加える機能 #

このディザはRPDFディザです。

# 更新履歴 #

## BpsConvWin 1.1.8 ##
24bit WAVファイルの読み込み

## BpsConvWin 1.1.7 ##
ノイズシェイピング機能

## BpsConvWin 1.1.5 ##
ノイズシェイピング機能を追加したがモノラル1チャンネルWAV以外の時は変なデータが出てくるバグあり。また処理選択チェックボックスの動作が変である