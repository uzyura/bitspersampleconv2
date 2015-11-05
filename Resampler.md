# サンプリングレート変換プログラムの性能比較 #

# 方法 #

1. SqWave2を使用して
  * サンプリング周波数44100Hz
  * 量子化ビット数16bit
  * 2kHzの矩形波
  * -10dB
  * 30秒
のWAVファイルを出力

2. 各コンバーターを使用して、96000HzのWAVに変換。

3. WaveSpectraでWAVファイルを開き、スペクトル図を見て、関係ない周波数が出ているかどうかを見る。

WaveSpectraの設定は
  * 縦軸 dB レンジ120dB
  * FFT サンプルデータ数 65536
  * 窓関数 ブラックマン-ハリス

# 結果 #

## Secret Rabbit Code libsamplerate-0.1.7のsndfile-resample ##

http://www.mega-nerd.com/SRC/

コンバータータイプはBest Sinc Interpolatorを指定。
```
$ sndfile-resample -to 96000 -c 0 from.wav to.wav
```

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_sndfileResample.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_sndfileResample.png)

## foobar2000用 Secret Rabbit Code v 1.0.3 ##

http://www.mega-nerd.com/SRC/fb2k.html

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000SRC.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000SRC.png)

## Resample 1.8.1 ##

https://ccrma.stanford.edu/~jos/resample/
```
$ resample -to 96000 from.wav to.wav
```
25kHz付近に-50dBくらいの、元の信号には存在しない成分が生じています。
また、出力レベルが低い。

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_resample.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_resample.png)

## foobar2000 v1.0.3のResampler ##

foobar2000 でWAVファイルを右クリックし、Convert→DSP欄でResamplerを選択
プロパティでサンプリングレートを96000に設定。

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000.png)

## foobar2000 v1.0.3のResampler Ultra mode ##

foobar2000 でWAVファイルを右クリックし、Convert→DSP欄でResamplerを選択
プロパティでサンプリングレートを96000に設定。Ultraのチェックを入れる。

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000UltraMode.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_foobar2000UltraMode.png)

## foobar2000用 SSRC 0.57 ##

http://otachan.com/foo_dsp_ssrc.html

![http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_SSRC.png](http://bitspersampleconv2.googlecode.com/files/44100_2kRect_to_96000_by_SSRC.png)


## 比較用Animated GIF ##

3秒に1回切り替わります

![http://bitspersampleconv2.googlecode.com/files/resamplerCompared.gif](http://bitspersampleconv2.googlecode.com/files/resamplerCompared.gif)


# 総括 #

この中ではSecret Rabbit Code libsamplerate-0.1.7のsndfile-resample
が最高性能。この測定条件の変換に関しては、これ以上の性能改善の余地はなさそうである。

foobar2000用Secret Rabbit Code plugin v1.0.3は
Secret Rabbit Code libsamplerate-0.1.7とは出力データが違っており、わずかに劣るようである。
だが、たいした違いではない。

foobar2000付属のResamplerは十分に良い。
また、Ultraのチェックボックスをチェック状態にすると、違うデータが出てくる。
性能はチェックをはずした状態と大差ない。

foobar2000用SSRCプラグインは、Secret Rabbit Code libsamplerate-0.1.7ほどではないが、良い。
たいした違いではないように見える。

上記リサンプラーの性能差は僅差であり、どれでもいい感じである。

これら調査したリサンプラーの中で、Resample 1.8.1だけは出力レベルが変わっており、
関係ない周波数成分が-50dBぐらい発生しているのでおすすめできない。

ここまで書いて、もっと徹底的に調べているページを発見

http://src.infinitewave.ca/