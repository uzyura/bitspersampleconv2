# Sound Blaster X-Fi Titanium HD OPAMP交換 #

Sound Blasterのオペアンプを交換し、RMAA 6.2.3を用いて測定を行い、諸性能を比較します。

1個の(同じカードの)Sound Blasterを使用し、幾つかオペアンプを交換し、RMAAした結果を比較します。

交換したオペアンプは、DACに近い2個です。I-V変換用のオペアンプです。

出力端子に近いほうの2個は、デフォルトのLME49710のままです。

# テストPCの構成 #

  * マザーボードはX58マザーのIntel DX58SO。BIOSリビジョンは5200。
  * CPUはIntel i7 980X。
  * グラボはGeforce GTX 260。
  * オンボードサウンドのRealtek 889Aは有効のまま。
  * Sound Blasterを挿したスロットは下から2番目のx1スロットです。
  * 他にサウンドカードAudioTrak Prodigy HD2を1個上のPCIスロットに挿しています。
  * IEEE1394でRME FireFace400とM-AUDIO Profire2626がデイジーチェーン接続しています。
  * ATX電源はSeasonic SS-550HT。
  * Intel X25-M 160G2GC
  * OSはWindows7の64ビット版です。

ちなみに、SB X-Fi Ti HDを2枚挿すと、OSのブートまで行くが、ブルースクリーンが発生します。
PC1台につき1枚までにしましょう。

# 設定 #

24bit 96kHz DirectSound出力 RCAアナログ出力/MME入力 RCAアナログ入力(AUX入力)のループバックで測定。

RCAケーブルは長さ1mの赤白ケーブル。

## Windowsミキサーの設定 ##

Windows7のミキサーは、ミキシング処理の際に、決められたサンプリング周波数(デフォルトは48kHz)に変換します。
この変換アルゴリズムが駄目すぎる(Apple MacOSXのCoreAudioに負けている)ため音質が劣化します。

RMAAを用いて、MMEやDirectSoundで24ビット、96kHzの測定をする際には
再生デバイス、録音デバイスの[プロパティ][詳細][既定の形式]を24bit 96000Hz
に合わせる必要があります。

## Sound Blasterの設定 ##

Sound BlasterはデフォルトでEAXエフェクトが有効になっています。
RMAA測定結果が悪くなるので
[スタート][すべてのプログラム][Creative](Creative.md)[Creativeオーディオコントロールパネル]
[EAXエフェクト][エフェクトを有効にする](EAX.md) のチェックを外します。

CMSS 3Dエフェクトもオフにする。

[ビットマッチ][ビットマッチ レコーディングを有効にする]をチェックする。

[ビットマッチ][ビットマッチ プレイバックを有効にする]をチェックする。

# RMAA測定結果 #

## 諸特性比較表 ##

|オペアンプ＼テスト内容|周波数特性 (40Hz～15kHz), dB|雑音レベル, dB(A)|ダイナミックレンジ, dB(A)|全高調波歪率, ％|IMD+Noise, ％|ステレオクロストーク, dB|取得価格(2個あたり)|
|:----------|:---------------------|:-----------|:---------------|:--------|:-----------|:-------------|:----------|
|LM358N     |+0.02,-0.11           |-112.5      |112.3           |0.192    |0.384       |-110.4        |33円        |
|NJM072D    |+0.02,-0.11           |-115.9      |115.8           |1.548    |0.759       |-110.6        |100円       |
|NJM4558DD  |+0.02,-0.11           |-115.4      |115.1           |0.0023   |0.0026      |-112.1        |50円        |
|NJM5532DD  |+0.02,-0.11           |-116.9      |116.9           |0.0020   |0.0024      |-111.9        |150円       |
|NJM2114D   |+0.02,-0.11           |-116.1      |116.5           |0.0020   |0.0025      |-110.1        |(140円)     |
|LME49720NA |+0.02,-0.11           |-115.4      |115.8           |0.0019   |0.0023      |-111.7        |540円       |
|OPA2134PA  |+0.02,-0.11           |-116.9      |116.9           |0.0020   |0.0024      |-112.4        |1,072円     |
|OPA2604AP  |+0.02,-0.11           |-116.7      |117.0           |0.0019   |0.0024      |-113.3        |1,360円     |

## 周波数特性(Frequency response) ##
| ![http://bitspersampleconv2.googlecode.com/files/fr1.png](http://bitspersampleconv2.googlecode.com/files/fr1.png) |![http://bitspersampleconv2.googlecode.com/files/fr2.png](http://bitspersampleconv2.googlecode.com/files/fr2.png)|
|:------------------------------------------------------------------------------------------------------------------|:----------------------------------------------------------------------------------------------------------------|

(4本の線が完全に重なっていて、最後に書いたマゼンタ色の線だけが書かれているように見えます。)

縦軸のスケールをよく見て下さい。20Hzでわずか-0.2dBのレベル低下です。

## 雑音レベル(Noise level) ##
| ![http://bitspersampleconv2.googlecode.com/files/ns1.png](http://bitspersampleconv2.googlecode.com/files/ns1.png)| ![http://bitspersampleconv2.googlecode.com/files/ns2.png](http://bitspersampleconv2.googlecode.com/files/ns2.png)|
|:-----------------------------------------------------------------------------------------------------------------|:-----------------------------------------------------------------------------------------------------------------|

## ダイナミックレンジ(Dynamic range) ##
|![http://bitspersampleconv2.googlecode.com/files/dr1.png](http://bitspersampleconv2.googlecode.com/files/dr1.png)|![http://bitspersampleconv2.googlecode.com/files/dr2.png](http://bitspersampleconv2.googlecode.com/files/dr2.png)|
|:----------------------------------------------------------------------------------------------------------------|:----------------------------------------------------------------------------------------------------------------|

## 全高調波歪率(Total harmonic distortion) ##
|![http://bitspersampleconv2.googlecode.com/files/thd1.png](http://bitspersampleconv2.googlecode.com/files/thd1.png)|![http://bitspersampleconv2.googlecode.com/files/thd2.png](http://bitspersampleconv2.googlecode.com/files/thd2.png)|
|:------------------------------------------------------------------------------------------------------------------|:------------------------------------------------------------------------------------------------------------------|

## 相互変調歪(IMD+Noise) ##
|![http://bitspersampleconv2.googlecode.com/files/imd1.png](http://bitspersampleconv2.googlecode.com/files/imd1.png)|![http://bitspersampleconv2.googlecode.com/files/imd2.png](http://bitspersampleconv2.googlecode.com/files/imd2.png)|
|:------------------------------------------------------------------------------------------------------------------|:------------------------------------------------------------------------------------------------------------------|

## ステレオクロストーク(Stereo crosstalk) ##
|![http://bitspersampleconv2.googlecode.com/files/ct1.png](http://bitspersampleconv2.googlecode.com/files/ct1.png)|![http://bitspersampleconv2.googlecode.com/files/ct2.png](http://bitspersampleconv2.googlecode.com/files/ct2.png)|
|:----------------------------------------------------------------------------------------------------------------|:----------------------------------------------------------------------------------------------------------------|

# 測定結果についての考察 #

NJM2114Dは購入時に搭載されていたオペアンプです。これは十分な性能がある。
DACでの音の味付け(coloration)を好まない方や、高性能なDACを求めている方は、交換する必要はないです。

オペアンプ交換の醍醐味は、変なオペアンプに交換して、キャラクターを楽しむものであるといえましょう。

NJM2114D、OPA2604、OPA2134PA、LME49720NA、NJM5532DD、NJM4558DD、OPA2134は互角。

OPA2134とNJM072DはFET入力で、他のオペアンプはバイポーラー入力です。
この回路はJFETでもbipolarでも正常動作するようです。

OPA2604は±4.5V以上の電源が必要なオペアンプです。正常動作している。

NJM4558DDは￥25です。これは安くて高性能といえる。

OPA2604や、OPA2134は本来もっと高性能のはずである。回路がこれらのオペアンプに最適化されていないのか、この回路のI-V変換にはあまり高性能なオペアンプは必要ないのか、とにかく性能が発揮出来ていない。あるいは、録音回路が再生回路の性能を下回っていて、再生回路の性能向上がグラフに現れていないという可能性が考えられる。

全高調波歪率やIMD＋Noiseが0.0001％単位でふらついているのは測定誤差だろう。

NJM072Dは他のオペアンプよりも、出力レベルが1dBほど低下する。
これは特徴的なキラキラした音が出る。
諸特性が他のオペアンプよりも悪い理由は、この回路はNJM072Dの性能が生かせないということです。
NJM072Dの性能が悪いわけではないです。

OPA2604は、ICから出ているピンに弾力があり、高級感がある。

LM358Nは、オーディオ用ではありませんが、予想よりも良い性能です。
オーディオDACのI-V変換用に使う人はいないと思いますが。

# ASIO入出力との比較 #

このページにASIO出力の場合のデフォルトオペアンプでのRMAAが載っています。
同傾向です。

http://av.watch.impress.co.jp/docs/20100524/96khz.htm

Windowsミキサーのマスターサンプルレートを96kHzに設定して
96kHzでの性能を測定すれば、
MMEやDirectSoundでもASIOに迫る性能が出るみたいです。
どう考えてもASIOの方が簡便で合理的ですが

# 音質に関して #

EAXをオフにするなど、設定をしっかり行えば、非常に良い音が出るサウンドカードです。
高級感のあるサウンドです。

正直なところ、オペアンプ交換によって、音質はあまり変わりません。
耳で聴くのではなく測定によって判断した方が定量的、客観的な答えが出る。
少し聴いた限りでは、私の耳は歪率1.5％と0.002％の音の違いを、確信を持って聴き分けることができなかった。
体調や音楽ソースによって変わるかもしれませんが。

以下に、針小棒大、プラシーボ全開の主観評価を書きます。

出力端子にゼンハイザーHD650を接続して
Foobar2000 0.9＋ASIO support pluginで試聴。

  * OPA2604は冷静で、大人しい感じである。OPA2604のデータシートを見ながら聞くと、良い音に聞こえる。
  * NJM5532は、明るい感じである。アキュフェーズC-3800にも使われている、ということを考えながら聞くと、良い音に聞こえる。
  * OPA2134はJ-FET入力ということだが、特段どうということもなく、他と変わらず、自然である。
  * LME49720NAとNJM4558DDとNJM2114Dも、自然である。OPA2134とどう違うかとというと、正直分からない。
  * NJM072Dは、輝かしい音がする。歪率1.5％というのは、一聴してわからない。音楽がフォルテになるとやかましくなる感じがする。だが、気のせいかもという程度。
  * LM358Nは、オーディオ用ではないが、意外にも普通に鳴る。NJM072Dと同様、音楽がフォルテになるとやかましくなる感じがする。歪っぽい音とはどういう音か知ることができるので、買っておいても良いかもしれない。安いし。

# FET入力とバイポーラーTr入力による特性の違いについて #

OPA2134PAとNJM072DはFET入力、NJM2114DはバイポーラーTr入力です。
以下の図の通り、NJM2114DとOPA2134PAは相互変調歪の特性が互角です。
NJM072Dは著しく悪い。

この回路では、バイポーラー有利とか、FET有利とか、そういうことはないようです。
それよりも、FET入力同士、バイポーラ入力同士のなかでの、オペアンプの品種による差のほうが大きい。

![http://bitspersampleconv2.googlecode.com/files/imdjfet.png](http://bitspersampleconv2.googlecode.com/files/imdjfet.png)

# インパルス応答波形と周波数-位相特性について #

## インパルス応答波形の理想の形状について ##

(サンプリング周波数96kHz、量子化ビット数24ビット)

ずーっと0がつづいていて、1サンプルだけ＋8388607(最大値)になり、またずーっと0が続くというデジタルデータをDACに入力します。
すると、アナログ出力には、以下の「理想的な出力信号波形」のような出力波形が出るのが理想です。裾野のビヨビヨは理想的には無限に広がります。

この波形をインパルス応答波形というのかどうかについては、怪しいですが、
ここではそう呼ぶことにします。

| 理想的な出力信号波形 |
|:-----------|
| ![http://bitspersampleconv2.googlecode.com/files/pulseinput.png](http://bitspersampleconv2.googlecode.com/files/pulseinput.png)|

## Sound Blaster X-FiTiHDの出力波形をオシロスコープで観察 ##

サウンドブラスターのRCAアナログ音声出力端子にオシロスコープのプローブを当てて測定しました。

使用したオシロスコープのは SoftDSP社のSDS 200Aです。
このオシロは200MHz、電圧軸の分解能48dB(256段階)という性能です。

↓無負荷時 ASIO出力 RCA出力端子 左チャンネル 96kHz 24bit。

| NJM2114D | NJM072D |
|:---------|:--------|
|![http://bitspersampleconv2.googlecode.com/files/2114_pulse.png](http://bitspersampleconv2.googlecode.com/files/2114_pulse.png)|![http://bitspersampleconv2.googlecode.com/files/072_pulse.png](http://bitspersampleconv2.googlecode.com/files/072_pulse.png)|

左のほうに1と書いてあるマーカーがある位置が0Vです。縦軸は500mV/DIV、横軸は40μs/DIVです。

波形の形状を観察すると、デジタルフィルターはシャープロールオフ型であり、LPFによる位相回転の影響は見られません。素直な形状です。

左側が下がっているのは、測定ミスです(ACレンジに設定していた!）
暇なときに測り直す必要ありです。

このグラフを見ながら音楽を聞くと、NJM2114Dは、立ち上がりの速い、瞬発力のある音に聴こえる気がするかもｗ

NJM072Dは明らかにピーク電圧が低いです。
こういうのは、スルーレートが関係しているんでしょうかね。
ともかく、この回路には、NJM072Dは向いていないようです。

## 1kHz矩形波(もどき)出力波形 ##

原信号波形が完全な矩形にならない(肩が斜めになっていて、頂上がぶよぶよとしている)理由は、DACが出力できる周波数が有限だから(無限に高くしていっても矩形波には近づきませんが…)です。矩形波生成プログラムにWaveGeneではなくSqWave2を使用している理由は、[SqWave2とWaveGeneの 10kHz 矩形波出力波形とスペクトル比較](http://code.google.com/p/bitspersampleconv2/wiki/SquareWaveOutput)をご覧ください。

| 原信号波形 SqWave2 で生成した96kHzサンプリングの1kHz矩形波を、図示のためにsndfile-resampleで8倍オーバーサンプルしたもの。 |
|:-------------------------------------------------------------------------------|
| ![http://bitspersampleconv2.googlecode.com/files/1kHzRect96x8.png](http://bitspersampleconv2.googlecode.com/files/1kHzRect96x8.png)|

↓ 無負荷時  SqWave2でASIO出力 RCA出力端子 左チャンネル 96kHz 16bit

|NJM2114D | NJM072D |
|:--------|:--------|
|![http://bitspersampleconv2.googlecode.com/files/2114_1kHzRect.png](http://bitspersampleconv2.googlecode.com/files/2114_1kHzRect.png)|![http://bitspersampleconv2.googlecode.com/files/072_1kHzRect.png](http://bitspersampleconv2.googlecode.com/files/072_1kHzRect.png)|

↓ 100pF容量負荷 SqWave2でASIO出力 RCA出力端子 左チャンネル  96kHz 16bit

| NJM2114D | NJM072D |
|:---------|:--------|
| ![http://bitspersampleconv2.googlecode.com/files/2114_1kHzRect100pf.png](http://bitspersampleconv2.googlecode.com/files/2114_1kHzRect100pf.png)|![http://bitspersampleconv2.googlecode.com/files/072_1kHzRect100pf.png](http://bitspersampleconv2.googlecode.com/files/072_1kHzRect100pf.png)|

わずかに右肩下がりになっているのは、測定ミスです(ACレンジに設定していた!）
暇なときに測り直す必要ありです。

## 10kHz正弦波出力波形 ##

↓ 無負荷時 SqWave2でASIO出力 RCA出力端子 左チャンネル  96kHz 16bit

| NJM2114D | NJM072D |
|:---------|:--------|
|![http://bitspersampleconv2.googlecode.com/files/2114_10kHzS.png](http://bitspersampleconv2.googlecode.com/files/2114_10kHzS.png)|![http://bitspersampleconv2.googlecode.com/files/072_10kHzS.png](http://bitspersampleconv2.googlecode.com/files/072_10kHzS.png)|

## 10kHz正弦波出力時スペクトログラム ##

↓ 無負荷時 SqWave2でASIO出力 RCA出力端子 左チャンネル  96kHz 16bit

| NJM2114D |
|:---------|
|![http://bitspersampleconv2.googlecode.com/files/2114_10kHzFFT.png](http://bitspersampleconv2.googlecode.com/files/2114_10kHzFFT.png)|

| NJM072D |
|:--------|
|![http://bitspersampleconv2.googlecode.com/files/072_10kHzFFT.png](http://bitspersampleconv2.googlecode.com/files/072_10kHzFFT.png)|


## 考察 ##

使用したオシロスコープは SoftDSP社のSDS 200Aです。
このオシロスコープは200MHz、電圧軸の分解能48dB(256段階)という性能です。

すべての波形グラフは、1秒間の残光表示設定になっていますので、
もし発振があれば、線が縦に太くなります。

発振の兆候は見られませんでした。

ダールジール等の無帰還アンプの音が好みの方には、NJM072Dを装着するとIMD特性が似るので、NJM072Dへの交換を試してみると良いかもしれません。ダールジールはスルーレートが優れていて、NJM072Dは優れていないので、出てくる音が似るかどうかはわかりませんが

  * http://stereophile.com/integratedamps/dartzeel_cth-8550_integrated_amplifier/index4.html の図9
  * http://www.stereophile.com/solidpoweramps/405dartzeel/index4.html の図6

# クロックジッターによる音質劣化について #

同一のサウンドカードで録再すると、クロックが同一のため、
ジッタによる影響を受けにくく
実際よりもダイナミックレンジ性能が高く出ることがあるという意見があります。

(2010年8月29日加筆)

私は、この意見に付いては、その通りであるという気がしてきました。
暇なときに、別のデバイスで録音して、測定してみて、全高調波歪率グラフの出方を比較してみようかと思っています。

クロックジッターが増すと、ダイナミックレンジや全高調波歪率が悪化します。
また、同一のクロックジッタ量のクロックを使用している状況で、
量子化ビット数を16ビットから24ビットに増やすと、ジッターの影響を256倍(48dB)受けやすくなります。

ジッターの影響が最もよく現れる図は、全高調波歪率グラフです。1kHzの正弦波テスト信号のピークの周りの裾野が広がったり、いくつも山ができたりします。以下のStereophileの記事をご覧ください:
http://www.stereophile.com/reference/1290jitter/

実際のジッターの例。以下のStereophileの記事の図10～図11あたりの解説記事が興味深いです。
http://stereophile.com/hirezplayers/dcs_puccini_sacd_playback_system/index6.html

# 同一PCのループバックで録再を行うと、実際の性能とは異なる結果が出る問題について #

普通、再生デバイスの測定を行う場合には、録音側は、正確な測定ができるよう校正された測定器を使います。そうしないと、基準がなくなって何を測っているのかわからなくなるためです。

前掲の同一PCの同一カードでの録再結果が、実際の性能とは異なる結果が出るというのは、
それはその通りですが、同一PC、同一オーディオIFの自己録再で、オペアンプを交換してRMAA結果を相対比較することには、それなりに意味があると思います。

もっと高性能な録音デバイスを使用して録再すれば、オペアンプの性能差がもっとはっきりと現れて、自己録再結果とは全く違った結果になるかもしれません。

# ADCの入力インピーダンスが一般的なアンプの入力インピーダンスよりも高いために、アンプと接続した場合よりもRMAAの結果がよくなる問題について #

Creative公式ページの情報によれば
```
(24bit 96kHz)

DACのS/N比
？(50kΩ負荷時か？) 122dB
330Ω負荷時 117dB
33Ω負荷時 115dB

なお、ADCのS/N比 118dB
```

とのことです。

アンプの入力インピーダンスは小さいものでも10kΩぐらいだろうから、問題ないと思われます。

この問題が気になる方は、RCAケーブルのHOT-GND間に10kΩ前後の抵抗をつなげてRMAA測定を行い、無負荷時と性能を比較すればよろしいかと思います。

# RCAケーブルの長さと音質の関係について #

RCA赤白ケーブル(長さ50cm、1m、5m)でRMAA結果を比較しました。

## 結果 ##

### 諸特性表 ###

|測定値＼RCAケーブル長さ| 0.5m | 1m     | 5m     |
|:------------|:-----|:-------|:-------|
|周波数特性(40 Hz～15 kHz), dB|+0.02, -0.11|+0.02, -0.11|+0.02, -0.11|
|Noise level, dB (A)  | -116.1 | -115.6 | -116.4 |
|Dynamic range, dB (A)| 116.0  | 115.6  | 116.5  |
|THD, %               | 0.0020 | 0.0020 | 0.0020 |
|IMD + Noise, %       | 0.0025 | 0.0025 | 0.0026 |
|Stereo crosstalk, dB | -111.2 | -110.0 | -110.8 |

### 周波数特性図 ###

(3本の線が完全に重なっていて、最後に書いた水色の線だけが書かれているように見えます。)

![http://bitspersampleconv2.googlecode.com/files/fr3.png](http://bitspersampleconv2.googlecode.com/files/fr3.png)

## 考察 ##

50cmのケーブルはステレオクロストーク性能が優れます。

ステレオクロストーク以外の諸性能は、あまり変わりませんでした。

# ASIOについて #

Creative X-Fi Titanium HDはASIOに対応しているのが良いです。

出力バッファサイズを32768バイトまで増やせるため、遅いPCでも音飛びしにくいです。

詳しくは[ASIO](ASIO.md)にまとめてあります。

# オペアンプ音質比較資料 #

Samuel Gronerさんがまとめたオペアンプ測定資料。これは良い資料ですよ
http://www.sg-acoustics.ch/analogue_audio/ic_opamps/pdf/opamp_distortion.pdf

Walter Jungさんのページ。Jungさんは、アナログ・デバイセズでオペアンプを作っている人です。
詳しく見てませんが、ここになにかあるかも
http://waltjung.org/index.html