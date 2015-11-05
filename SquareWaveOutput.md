# SqWave2とWaveGeneの10kHz 矩形波出力波形とスペクトル比較 #

  * サンプリング周波数192kHz
  * 量子化ビット数16bit
  * 出力レベル-10dB
  * 10kHz 矩形波
という条件でSqWave2とWaveGeneの出力波形とスペクトルを比較しました。

## 図1-1 出力データの比較(192kHzサンプリング、矩形波、10kHz、-10dB) ##

![http://bitspersampleconv2.googlecode.com/files/raw.png](http://bitspersampleconv2.googlecode.com/files/raw.png)

図1-1は出力データ(WAVファイル)のPCMデータ サンプル値をそのままグラフにプロットしたもので、縦軸がサンプル値、横軸が時間です。
赤い＋点がSqWave2の出力データ、緑の×点がWaveGeneの出力データです。
グラフの一番左 0のところは0サンプル目(最初のサンプル値)です。DACのアナログ出力にこの波形が出るわけではないです。(DACの出力波形の綺麗さについては図1-2～図1-4で比較できます。)

WaveGeneの出力である緑の×印をよく見ると、×が連続している個数が、5分の2が9サンプル、5分の3が10サンプルになっています。これは10kHzが192kHzで「割り切れない」ため、矩形波の半波長5個分の時間に、192000分の9秒の半波長を2回、192000分の10秒を3回含むような山谷を繰り返すことで、全体として10kHzの音が出るようにしているのです。これによって、半波長ごとに時間長さが伸び縮みする波形が出てきます。そのため、オシロスコープで観察すると不安定な波形が出力されているように見えます。

SqWave2の出力である赤色の＋点は、一見乱雑にプロットされているように見えますが、これらすべての点は10kHz矩形波のフーリエ級数をナイキスト周波数近くまで足しこんでできる連続波形の上に乗っているのです。そのため、これを理想的なDACに入力すると、オーバーサンプル処理とLPF処理によって「10kHz矩形波のフーリエ級数をナイキスト周波数近くまで足しこんでできる連続波形」が完全に再現されアナログ音声出力から出てきます。

## 図1-2 DACのアナログ出力波形シミュレーション。図1-1のデータに8倍オーバーサンプリング処理した波形 ##

![http://bitspersampleconv2.googlecode.com/files/8x.png](http://bitspersampleconv2.googlecode.com/files/8x.png)

縦軸がサンプル値、横軸が時間です。
出力レベルが小さい赤い線がSqWave2の出力データ、
出力レベルが大きい緑の線がWaveGeneの出力データです。
グラフの一番左 0のところは0サンプル目(最初のサンプル値)です。

教科書通りの8倍オーバーサンプリングを行うDACのアナログ出力からは、
大体図1-2のような波形が出ます。
SqWave2の出力波形はWaveGeneよりも安定しています。

最初の半波長ぐらいはどちらも波形が乱れています。この理由は、~~オーバーサンプリング処理は、
注目サンプル点の前にも後ろにも一定数のサンプル値が必要で、データが送られ始めたばかりの段階では十分なデータが集まらないため補間の精度が低下するためでしょう。~~無信号状態から急に信号が始まる境界点に、ナイキスト周波数の範囲内に収まらない高周波成分が含まれるために、完全に再現できないためだそうです。

なお、オーバーサンプリングはsndfile-resampleを用いて以下のようにして行いました。
sndfile-resampleは極めて高精度なリサンプルができるのでおすすめです。
```
sndfile-resample -by 8 -c 0 SQ10K_192.wav SQ10K_1536.wav
sndfile-resample -by 8 -c 0 WG10K_192.wav WG10K_1536.wav
```

## 図1-3 DACの出力波形をオシロスコープで観察。SqWave2 10kHz矩形波 ASIO出力 M-AUDIO ProFire2626のアナログ音声出力波形 ##

![http://bitspersampleconv2.googlecode.com/files/SQ10KOSC.png](http://bitspersampleconv2.googlecode.com/files/SQ10KOSC.png)

オシロスコープはSoftDSP社のSDS-200Aです。  M-AUDIO ProFire2626のアナログ音声出力から安定した波形が出ています。

## 図1-4 DACの出力波形をオシロスコープで観察。WaveGene 10kHz矩形波 ASIO出力 M-AUDIO ProFire2626のアナログ音声出力波形 ##

![http://bitspersampleconv2.googlecode.com/files/WG10KOSC.png](http://bitspersampleconv2.googlecode.com/files/WG10KOSC.png)

半波長ごとに時間長さが変化する複雑な波形のため、オシロスコープがうまく同期しません。

なお、出力レベルが大きいので、縦軸のスケールを半分にしました。


## 図1-5 SqWave2の10kHz 矩形波出力データのスペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/spectrum.png](http://bitspersampleconv2.googlecode.com/files/spectrum.png)

(SqWave2の10kHz 矩形波出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

## 図1-6 WaveGeneの10kHz 矩形波出力データのスペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/spectrum_wg.png](http://bitspersampleconv2.googlecode.com/files/spectrum_wg.png)

(WaveGeneの10kHz 矩形波出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

WaveGeneの出力データはエイリアシング歪が生じています。
これだけ大きな音量で関係ない周波数成分が含まれていると、耳で聞いてもわかります。

# 割り切れる周波数比の場合の出力波形比較 #

  * 9600Hz 矩形波
  * -10dB
  * 192kHzサンプリング
  * 量子化ビット数16bit
という条件で比較しました。

## 図1-7 出力サンプル値　最初の100サンプルをプロットしたもの ##

![http://bitspersampleconv2.googlecode.com/files/9600.png](http://bitspersampleconv2.googlecode.com/files/9600.png)

どちらも、4分の1波長ぶんのデータがあればそれを左右対称、上下対称に展開して残りのデータを作れるような、シンメトリーなデータが出てきています

## 図1-8 出力サンプル値　最初の100サンプルを8倍オーバーサンプルし、実線でつないだもの ##

![http://bitspersampleconv2.googlecode.com/files/9600x8.png](http://bitspersampleconv2.googlecode.com/files/9600x8.png)

## 図1-9 SqWave2の9600Hz -10dB  矩形波出力データのスペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/SQ9600_192.png](http://bitspersampleconv2.googlecode.com/files/SQ9600_192.png)

(図1-7の出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

## 図1-10 WaveGeneの9600Hz -10dB 矩形波出力データのスペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/WG9600_192.png](http://bitspersampleconv2.googlecode.com/files/WG9600_192.png)

(図1-7の出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

# 考察 #

9600Hz 矩形波　192kHzサンプリングという条件では、SqWave2もWaveGeneも可聴域に関係ない周波数成分は含まれていません。

`!WaveGeneは出力レベルが間違っています。`

第5次高調波成分 48000Hzのところにカーソルを合わせて出力レベル値を比べました(図9と図10参照)

  * SqWave2は第5次高調波が第1次高調波の-14dB
  * WaveGeneは第5次高調波が第1次高調波の-13dB

矩形波のフーリエ級数の式によると、第5次高調波は、5分の1 = -13.98dB含まれているのが正しい。

したがって割り切れる周波数比のような条件でも、
WaveGeneよりもSqWave2のほうが正確な出力波形が出るみたいです。

成分比はSqWaveの方が正確ですが、SqWaveの出力レベルの絶対値が正しいかどうかは
どうも怪しいようです。

# なぜフーリエ級数の正弦波加算処理をナイキスト周波数で打ち切る必要があるのか #

SqWave2はフーリエ級数の正弦波加算処理をナイキスト周波数の99％で打ち切っています。

その理由は、ナイキスト周波数を超える成分を加算するとエイリアシング(折り返し雑音)が発生するからです。
エイリアシングとはどのような現象かというと、DACにナイキスト周波数+αの正弦波を入力すると
ナイキスト周波数-αの正弦波が出てくるという現象です。これは図にするとわかりやすいです(図2-1から図2-9)。

PCM音声信号はナイキスト周波数を超える正弦波の情報を表現する方法を持っていないのです。

エイリアシングを発生させないために、ナイキスト周波数を超える周波数成分は、デジタル領域で生成しないようにする必要があります。

## 図2-1 24kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##

↓DACに入力するPCMデータ

![http://bitspersampleconv2.googlecode.com/files/Sine24k.png](http://bitspersampleconv2.googlecode.com/files/Sine24k.png)

↓DACのアナログ出力波形(simulated) 以下図2-9まで同様です。

![http://bitspersampleconv2.googlecode.com/files/Sine24k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine24k_1536.png)

## 図2-2 36kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine36k.png](http://bitspersampleconv2.googlecode.com/files/Sine36k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine36k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine36k_1536.png)

## 図2-3 48kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine48k.png](http://bitspersampleconv2.googlecode.com/files/Sine48k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine48k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine48k_1536.png)

## 図2-4 60kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine60k.png](http://bitspersampleconv2.googlecode.com/files/Sine60k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine60k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine60k_1536.png)

## 図2-5 72kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine72k.png](http://bitspersampleconv2.googlecode.com/files/Sine72k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine72k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine72k_1536.png)

## 図2-6 84kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine84k.png](http://bitspersampleconv2.googlecode.com/files/Sine84k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine84k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine84k_1536.png)

以上(図2-1～図2-6)のように、ナイキスト周波数未満の正弦波信号は、綺麗にアナログ出力に出てきます。

## 図2-7 96kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##

![http://bitspersampleconv2.googlecode.com/files/Sine96k.png](http://bitspersampleconv2.googlecode.com/files/Sine96k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine96k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine96k_1536.png)

↑ここで破綻が起こります。ナイキスト周波数ぴったりの正弦波は、位相によって出力が0～1倍になります。

## 図2-8 108kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine108k.png](http://bitspersampleconv2.googlecode.com/files/Sine108k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine108k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine108k_1536.png)

↑108kHzの正弦波を作ったつもりが、PCMデジタルデータがすでに84kHzの正弦波(図2-6)の位相が180度ずれた情報と全く同じ情報になっています。DACのアナログ出力からは84kHzの正弦波が出ます。

この状況を「エイリアシング(折り返し雑音)が発生している」といいます。

## 図2-9 120kHz正弦波、192kHzサンプリング PCMデータのプロットとDACの出力波形シミュレーション ##
![http://bitspersampleconv2.googlecode.com/files/Sine120k.png](http://bitspersampleconv2.googlecode.com/files/Sine120k.png)

![http://bitspersampleconv2.googlecode.com/files/Sine120k_1536.png](http://bitspersampleconv2.googlecode.com/files/Sine120k_1536.png)

↑120kHzの正弦波を作ったつもりが、PCMデジタルデータがすでに72kHzの正弦波(図2-5)の位相が180度ずれた情報と全く同じ情報になっています。DACのアナログ出力からは72kHzの正弦波が出ます。

なお、エイリアシングの説明とは関係ないですが
図2-xのPCMデータプロットは0秒目から100サンプルのプロットですが、
図2-xのDAC出力波形シミュレーションの図は、sndfile-resampleで8倍オーバーサンプルしたもので、
正弦波出力開始から1秒経過後の波形をプロットしています。
オーバーサンプル処理は、前後に十分な数のデータが必要なので、
正弦波出力開始直後は出力が安定しないため1秒後の波形をプロットしました。

# ナイキスト周波数を超えて加算を継続したらどうなるか #

  * 10kHz矩形波
  * -10dB
  * 192kHzサンプリング
  * 量子化ビット数16bit

という条件下で、
加算打ち切り周波数を200％、10000％にそれぞれ設定し、出力データを観察します。

## 図3-1 加算打ち切り周波数 200％ 出力データをプロット ##

![http://bitspersampleconv2.googlecode.com/files/10kRect_200.png](http://bitspersampleconv2.googlecode.com/files/10kRect_200.png)

## 図3-2 加算打ち切り周波数 200％ スペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/10kRect_200_FFT.png](http://bitspersampleconv2.googlecode.com/files/10kRect_200_FFT.png)

(図3-1の出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

## 図3-3 加算打ち切り周波数 10000％ 出力データをプロット ##

![http://bitspersampleconv2.googlecode.com/files/10kRect_10000.png](http://bitspersampleconv2.googlecode.com/files/10kRect_10000.png)

## 図3-4 加算打ち切り周波数 10000％ スペクトログラム ##

![http://bitspersampleconv2.googlecode.com/files/10kRect_10000_FFT.png](http://bitspersampleconv2.googlecode.com/files/10kRect_10000_FFT.png)

(図3-3の出力データをWaveSpectraでFFTしました。
8倍オーバーサンプル後のデータではありません。)

## 図3-5 DAC出力波形シミュレーション ##

![http://bitspersampleconv2.googlecode.com/files/10kRect_beyond_nyquist_waveform.png](http://bitspersampleconv2.googlecode.com/files/10kRect_beyond_nyquist_waveform.png)

図3-1、図3-3のPCMデータを8倍オーバーサンプリングしたデータを実線でつないだもので、
赤線が打ち切り周波数200％、緑の線が10000％です。

図1-2の赤線と比較すると、図3-5の出力波形の形状は不安定です。

# 考察 #

図3-2、図3-4を見比べると、折り返し雑音が折り返しを繰り返しながら発生していく様子がよくわかります。

2kHzの折り返し雑音成分は1回目の折り返しで発生し、6kHzは2回目以降の折り返しで発生しているみたいです。

図3-1、図3-3を見比べると、打ち切り周波数をナイキスト周波数を超えてどんどん足しこんでいくと、10kHz矩形波を、10kHz正弦波の波高値が正ならば1、0ならば0、負ならば-1という計算式で生成したような出力データに近づくようです。
WaveGeneの矩形波出力データ(0は出てこない)とは波高値が0の時の処理が違うようですが、あとはよく似ています(図3-3と図1-1の緑のx点とを見比べてみて下さい)。

# ギブス現象(Gibbs phenomenon)について #

矩形波のフーリエ級数を無限に足していくと、立ち上がりと立ち下がりのヒゲが9％ぐらい飛び出た形状に近づくそうです(ギブス現象)。

このページの上のほうで散々見てきたように、現実のDACはサンプリング周波数が有限なので、矩形波を出力しようとすると、立ち上がりの傾きがなまっていて、台地が平坦でなくビヨビヨと揺れている、妥協した波形が出てきます。

しかし、純粋に数学的に、サンプリング周波数が無限に上げられる理想的なDACを想定したとしても、その理想的なDACで矩形波を出力しようとすると、立ち上がりと立ち下がりにかなり大きな(9％)ヒゲが出てしまい、矩形波に限りなく近づいていくわけではない、というのですから興味深いです。

http://ja.wikipedia.org/wiki/%E3%82%AE%E3%83%96%E3%82%BA%E7%8F%BE%E8%B1%A1



---


ASIO is a trademark of Steinberg Media Technologies GmbH.