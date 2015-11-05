# 説明 #

Wav2GnuplotはWAVファイルを読み込んでGnuplotでプロットできる形式に変換するプログラムです。
横軸がサンプルの番号、縦軸がサンプル値のグラフを出力することが出来ます。

Gnuplotで複数のWAVファイルの出力を1個のグラフにプロットすることが出来ます。

スピーカーケーブルやアンプの音質差がわかるかもしれないと思って作りました。
この可視化方式の最大の欠点は、人間の耳は波形の形で音を聞いているわけではなく、
音声を機械的フーリエ変換したものを聞いていて、その原理上位相情報が正確に伝わらないということです。
したがって入出力WAVファイルの波形が大きく異なっていても、
それが人間の耳には知覚できない違いである可能性があります。

音声データを直交ウェーブレットによるマッチング追跡したもの同士を比較するともう少しましな比較になるかもしれません。

# インストール方法 #

上のほうのDownloadsタブを押して、Wav2GnuplotSetupYYYYMMDD.msiをダウンロードしてインストールします。
あとは画面の指示に従って読み込むWAVファイルの名前を指定します。

![http://bitspersampleconv2.googlecode.com/files/Wav2Gnuplot2.jpg](http://bitspersampleconv2.googlecode.com/files/Wav2Gnuplot2.jpg)

2つのWAVを比較するときは、まずWavSynchroで演奏開始時間を合わせたWAVファイルを作り、
これを1つ1つWav2Gnuplotに読み込ませ、Gnuplot用データを作成します。

そしてGnuplotを起動し、以下のようにコマンドを入力します。
(11.wav.dat、22.wav.datがWav2Gnuplotで出力したデータとする。)

```
gnuplot> set terminal png large size 3040, 2560
gnuplot> set output "output2.png"
gnuplot> plot "11.wav.dat" with lines, "22.wav.dat" with lines
gnuplot> quit
```

3つのWAVファイルのサンプルデータをプロットするには、以下のようにします。
```
gnuplot> set terminal png large size 3040, 2560
gnuplot> set output "output2.png"
gnuplot> plot "11.wav.dat" with lines, "22.wav.dat" with lines, "33.wav.dat" with lines
gnuplot> quit
```

# アンインストール方法 #

コントロールパネルの[プログラムの追加と削除]から削除します。

# ソースコードの入手方法 #

上のほうのSourceのところから入手します。
Browseで閲覧することも出来ます。このあたりです
http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/Wav2Gnuplot/Wav2Gnuplot