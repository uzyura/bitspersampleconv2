# 説明 #

サイズが全く同じ2つのWAVファイルを読み込み、
1個目のWAVファイルの左チャンネルを左チャンネル、2個目のWAVファイルの左チャンネルを右チャンネルとしたWAVファイルを作成するプログラムです。

# 使用方法 #

あらかじめ.NET Framework 3.5を入れます。

2個のサイズが全く同じ2つのWAVファイルを(WavSynchroで切り出すなどして)
用意します。

このページの上のほうのDownloadsのタブにあるWavCompositionC.zipをダウンロードしてきて展開します。2個のWAVファイルが置いてあるところに展開したexeを置きます。

2つのWAVファイルが11.wav、33.wavだったとします。

コマンドプロンプトで2個のWAVファイルが置いてあるところにcdし、

```
> WavCompositionC -ch0 11.wav ch0 -ch1 33.wav ch0 11L33L.wav
```

のように入力すると11.wavの左チャンネルが左チャンネル、
33.wavの左チャンネルが右チャンネルになったWAVファイル 11L33L.wavができあがります。

```
> WavCompositionC -ch0 11.wav ch1 -ch1 33.wav ch1 11R33R.wav
```

のように入力すると11.wavの右チャンネルが左チャンネル、
33.wavの右チャンネルが右チャンネルになったWAVファイル 11R33R.wavができあがります。

# アンインストール方法 #

持ってきたexeファイルを削除します。

# ソースコード #

これです。
http://code.google.com/p/bitspersampleconv2/source/browse/trunk/WavCompositionC/WavCompositionC/Program.cs