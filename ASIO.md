ASIO is a trademark of Steinberg Media Technologies GmbH.

# ASIOの使用方法 #

ASIOは、PCM音声を録再するためのAPIです。

ASIOに対応したオーディオインターフェースを用意し、
Foobar2000＋ASIOプラグインといったASIO対応のソフトを使用し、
ASIOを使用して音楽を再生するような設定を行った上で、音楽ファイルを再生すると、
ようするに、元データに忠実な再生ができると期待できます。

# ASIO APIを使用したプログラムを作る #

http://www.steinberg.net/en/company/3rd_party_developer.html
から、ASIO SDKをダウンロードします。(無料)

あとは、適当なCコンパイラがあれば開発できます。
Visual C++ Express Editionは、ただで配っています。

ASIO APIを使用するサンプルプログラムは、ASIOSDK2のhostの中のsampleが
多少読みづらいですが、一応動くプログラムなので参考になると思います。

# ASIOを使用してPCM音声を再生する方法(概要) #

1. まず、ASIOデバイスを列挙し、アプリケーション使用者にどのデバイスを使用するか選択させます。選択されたデバドラをロードします。

2. デバドラを初期化し、ASIOバッファサイズを問い合せます。M-AUDIO Profireの場合、SInt32 LSBで最大4096サンプルと戻ってきます。このサンプル数の大きさは、究極的にはハードの持っているバッファの大きさなので、ハードウェアによって異なります。

3. ASIOは音声データがダブルバッファになっているので、ここで

```
bufferSize = SInt32=1サンプルあたり4バイト x 4096サンプル x ステレオ2ch
```

以上の計算で求めたbufferSizeのバイト数をもったバッファを2個用意します。

これを仮にbuffer0とbuffer1と名付けます。

この2つのバッファをASIOに登録します。

![http://bitspersampleconv2.googlecode.com/files/asio01.png](http://bitspersampleconv2.googlecode.com/files/asio01.png)

4. バッファ0に0サンプル目から4095サンプル目までの4096サンプル分のPCM音声データを書きこみます。バッファ0やバッファ1はメインメモリ上にあるので、C言語では、ただ代入文をforループするか、あるいはmemcpyするだけです。

5. ASIO APIを呼び出して再生を開始します。バッファ0に入っている音声データが再生されます。
すぐにBufferSwitchコールバックが来ます。ここでバッファ1に4096サンプル目から8191サンプル目までの4096サンプル分のPCM音声データを書きこみます。

バッファ0に入っている4096サンプル分の再生が終わるとバッファ1のデータの再生が始まると同時にBufferSwitchコールバックが発生します。

BufferSwitchコールバックを受け取ったら、
バッファ0はもう読み出されていないので、バッファ0に8192サンプル目から12287サンプル目までの4096サンプル分のPCM音声データを埋めます。この処理が、バッファ1の再生が終わるまでの間に間に合えば、音声は破綻なく再生できます。

# ASIO device parameters of various ASIO devices #



以下に様々なASIOデバイスのパラメータを列挙します。

min buffer sizeがどのデバイスも非常に少ない数になっています。たとえばProfireは64サンプルとありますが、サンプリングレート192kHzの場合、0.33ミリ秒分のデータしか入らず、0.33ミリ秒ごとにバッファを埋め直さなければなりません。これはWindowsやMac OSや普通のLinuxでは不可能で、リアルタイムスケジューラを持ったリアルタイムOSでなければ厳しい数字です。最近はコンシューマーデスクトップ向けOSで、Ubuntu RealtimeのようなリアルタイムOSが出てきたみたいですが、
0.33ミリ秒とか、そこまでのリアルタイム性は電子楽器ですらも要求されないので、余裕を持って大きめのサイズにする方が音切れの可能性が減って良いと思います。

|device\params|output buffer format|min buffer size       (bytes)|preferred buffer size (bytes)|max buffer size       (bytes)|internal clock generator name|Output ready supported|ASIO Control Panel| max output sample rate|
|:------------|:-------------------|:----------------------------|:----------------------------|:----------------------------|:----------------------------|:---------------------|:-----------------|:----------------------|
|Realtek ASIO |sint16 LSB          |88                           |352                          |1764                         |internal                     |Yes                   |Not exists        |192kHz                 |
|M-AUDIO ProFire 2626|sint32 LSB          |64                           |4096                         |4096                         |See Control Panel            |Yes                   |Exists            |192kHz                 |
|ASIO4ALL     |sint32 LSB          |64                           |256                          |2048                         |Big Ben                      |No                    |Exists            |?                      |
|Prodigy HD2  |sint32 LSB          |256(Default)                 |256(Default)                 |256(Default)                 |Prodigy HD2 Clock            |Yes                   |Not Exists but no error reported when asioControlPanel()|192kHz                 |
|Creative ASIO|sint32 LSB          |96                           |4800(Default)                |32768                        |Internal                     |Yes                   |Exists            |96kHz                  |
|RME Fireface400|sint32 LSB          |48                           |256(default)                 |1024                         |Settings                     |Yes                   |Not Exists but no error reported when asioControlPanel()|192kHz                 |

# RME Fireface400 #

```
  asioVersion:   0
  driverVersion: 1
  Name:          hdspfire
ASIOGetChannels() inputs=8 outputs=8
ASIOGetBufferSize() min=1024 max=1024 preferred=1024 granularity=0
ASIOSetSampleRate(sampleRate=192000.000000)
ASIOOutputReady() Supported
i= 0 ch= 0 isInput=1 chGroup=00000000 type=18 name=Analog 1 (1)
i= 1 ch= 1 isInput=1 chGroup=00000000 type=18 name=Analog 2 (1)
i= 2 ch= 2 isInput=1 chGroup=00000000 type=18 name=Analog 3 (1)
i= 3 ch= 3 isInput=1 chGroup=00000000 type=18 name=Analog 4 (1)
i= 4 ch= 4 isInput=1 chGroup=00000000 type=18 name=Analog 5 (1)
i= 5 ch= 5 isInput=1 chGroup=00000000 type=18 name=Analog 6 (1)
i= 6 ch= 6 isInput=1 chGroup=00000000 type=18 name=Analog 7 (1)
i= 7 ch= 7 isInput=1 chGroup=00000000 type=18 name=Analog 8 (1)
i= 8 ch= 0 isInput=0 chGroup=00000000 type=18 name=Analog 1 (1)
i= 9 ch= 1 isInput=0 chGroup=00000000 type=18 name=Analog 2 (1)
i=10 ch= 2 isInput=0 chGroup=00000000 type=18 name=Analog 3 (1)
i=11 ch= 3 isInput=0 chGroup=00000000 type=18 name=Analog 4 (1)
i=12 ch= 4 isInput=0 chGroup=00000000 type=18 name=Analog 5 (1)
i=13 ch= 5 isInput=0 chGroup=00000000 type=18 name=Analog 6 (1)
i=14 ch= 6 isInput=0 chGroup=00000000 type=18 name=Analog 7 (1)
i=15 ch= 7 isInput=0 chGroup=00000000 type=18 name=Analog 8 (1)
ASIOGetLatencies() input=1064 output=1312
ASIOGetClockSources() result=0 numOfClockSources=1
 idx=0 assocCh=0 assocGrp=0 current=1 name=Settings
```


# Creative SB X-Fi ASIO #
```
  asioVersion:   0
  driverVersion: 2
  Name:          SB X-Fi ASIO [0001]
ASIOGetChannels() inputs=18 outputs=18
ASIOGetBufferSize() min=96 max=32768 preferred=4800 granularity=8
ASIOSetSampleRate(sampleRate=96000.000000)
ASIOOutputReady() Supported
i= 0 ch= 0 isInput=1 chGroup=80000001 type=18 name=Mix FL
i= 1 ch= 1 isInput=1 chGroup=80000002 type=18 name=Mix FR
i= 2 ch= 2 isInput=1 chGroup=80000000 type=18 name=Mix RL
i= 3 ch= 3 isInput=1 chGroup=80000000 type=18 name=Mix RR
i= 4 ch= 4 isInput=1 chGroup=80000000 type=18 name=Mix FC
i= 5 ch= 5 isInput=1 chGroup=80000000 type=18 name=Mix LFE
i= 6 ch= 6 isInput=1 chGroup=80000000 type=18 name=Mix RC or SL
i= 7 ch= 7 isInput=1 chGroup=80000000 type=18 name=Mix RC or SR
i= 8 ch= 8 isInput=1 chGroup=00000001 type=18 name=Digital-In L
i= 9 ch= 9 isInput=1 chGroup=00000001 type=18 name=Digital-In R
i=10 ch=10 isInput=1 chGroup=00000000 type=18 name=Line-In 2/Mic 2 L
i=11 ch=11 isInput=1 chGroup=00000000 type=18 name=Line-In 2/Mic 2 R
i=12 ch=12 isInput=1 chGroup=00000000 type=18 name=Auxiliary 2 L
i=13 ch=13 isInput=1 chGroup=00000000 type=18 name=Auxiliary 2 R
i=14 ch=14 isInput=1 chGroup=00000000 type=18 name=Mic In L
i=15 ch=15 isInput=1 chGroup=00000000 type=18 name=Mic In R
i=16 ch=16 isInput=1 chGroup=00000001 type=18 name=SPDIF In L
i=17 ch=17 isInput=1 chGroup=00000001 type=18 name=SPDIF In R
i=18 ch= 0 isInput=0 chGroup=00000000 type=18 name=Front L/R
i=19 ch= 1 isInput=0 chGroup=00000000 type=18 name=Front L/R
i=20 ch= 2 isInput=0 chGroup=00000000 type=18 name=Rear L/R
i=21 ch= 3 isInput=0 chGroup=00000000 type=18 name=Rear L/R
i=22 ch= 4 isInput=0 chGroup=00000000 type=18 name=Front C/Sub
i=23 ch= 5 isInput=0 chGroup=00000000 type=18 name=Front C/Sub
i=24 ch= 6 isInput=0 chGroup=00000000 type=18 name=Rear C/Top
i=25 ch= 7 isInput=0 chGroup=00000000 type=18 name=Rear C/Top
i=26 ch= 8 isInput=0 chGroup=00000000 type=18 name=Side L/R
i=27 ch= 9 isInput=0 chGroup=00000000 type=18 name=Side L/R
i=28 ch=10 isInput=0 chGroup=00008000 type=18 name=FX 1 L/R
i=29 ch=11 isInput=0 chGroup=00008000 type=18 name=FX 1 L/R
i=30 ch=12 isInput=0 chGroup=00008000 type=18 name=FX 2 L/R
i=31 ch=13 isInput=0 chGroup=00008000 type=18 name=FX 2 L/R
i=32 ch=14 isInput=0 chGroup=00008000 type=18 name=FX 3 L/R
i=33 ch=15 isInput=0 chGroup=00008000 type=18 name=FX 3 L/R
i=34 ch=16 isInput=0 chGroup=00008000 type=18 name=FX 4 L/R
i=35 ch=17 isInput=0 chGroup=00008000 type=18 name=FX 4 L/R
ASIOGetLatencies() input=32868 output=32768
ASIOGetClockSources() result=0 numOfClockSources=1
 idx=0 assocCh=-1 assocGrp=-1 current=1 name=Internal
```

# Prodigy HD2 v1.08(64bit) #

```
  asioVersion:   0
  driverVersion: 2
  Name:          Prodigy HD2
ASIOGetChannels() inputs=4 outputs=4
ASIOGetBufferSize() min=256 max=256 preferred=256 granularity=0
ASIOSetSampleRate(sampleRate=96000.000000)
ASIOOutputReady() Supported
i= 0 ch= 0 isInput=1 chGroup=00000000 type=18 name=L:Prodigy HD2 1/2
i= 1 ch= 1 isInput=1 chGroup=00000000 type=18 name=R:Prodigy HD2 1/2
i= 2 ch= 2 isInput=1 chGroup=00000000 type=18 name=L:Prodigy HD2 3/4
i= 3 ch= 3 isInput=1 chGroup=00000000 type=18 name=R:Prodigy HD2 3/4
i= 4 ch= 0 isInput=0 chGroup=00000000 type=18 name=L:Prodigy HD2 1/2
i= 5 ch= 1 isInput=0 chGroup=00000000 type=18 name=R:Prodigy HD2 1/2
i= 6 ch= 2 isInput=0 chGroup=00000000 type=18 name=L:Prodigy HD2 3/4
i= 7 ch= 3 isInput=0 chGroup=00000000 type=18 name=R:Prodigy HD2 3/4
ASIOGetLatencies() input=256 output=256
ASIOGetClockSources() result=1 numOfClockSources=1
 idx=0 assocCh=0 assocGrp=0 current=1 name=Prodigy HD2 Clock
```

# Realtek ASIO #

```
  asioVersion:   0
  driverVersion: 2
  Name:          Realtek ASIO
ASIOGetChannels() inputs=6 outputs=8
ASIOGetBufferSize() min=88 max=1764 preferred=352 granularity=1
ASIOSetSampleRate(sampleRate=192000.000000)
ASIOOutputReady() Supported
i= 0 ch= 0 isInput=1 chGroup=00000000 type=16 name=HD Audio Line input #0
i= 1 ch= 1 isInput=1 chGroup=00000000 type=16 name=HD Audio Line input #1
i= 2 ch= 0 isInput=1 chGroup=00000001 type=16 name=HD Audio Mic input #0
i= 3 ch= 1 isInput=1 chGroup=00000001 type=16 name=HD Audio Mic input #1
i= 4 ch= 0 isInput=1 chGroup=00000002 type=16 name=HD Audio Stereo input #0
i= 5 ch= 1 isInput=1 chGroup=00000002 type=16 name=HD Audio Stereo input #1
i= 6 ch= 0 isInput=0 chGroup=00000000 type=16 name=HD Audio output #0
i= 7 ch= 1 isInput=0 chGroup=00000000 type=16 name=HD Audio output #1
i= 8 ch= 2 isInput=0 chGroup=00000000 type=16 name=HD Audio output #2
i= 9 ch= 3 isInput=0 chGroup=00000000 type=16 name=HD Audio output #3
i=10 ch= 4 isInput=0 chGroup=00000000 type=16 name=HD Audio output #4
i=11 ch= 5 isInput=0 chGroup=00000000 type=16 name=HD Audio output #5
i=12 ch= 6 isInput=0 chGroup=00000000 type=16 name=HD Audio output #6
i=13 ch= 7 isInput=0 chGroup=00000000 type=16 name=HD Audio output #7
ASIOGetLatencies() input=352 output=704
ASIOGetClockSources() result=0 numOfClockSources=1
 idx=0 assocCh=-1 assocGrp=-1 current=1 name=Internal
```

# M-AUDIO ProFire 2626 #

```
  asioVersion:   0
  driverVersion: 5082
  Name:          M-Audio FW ASIO
ASIOGetChannels() inputs=26 outputs=26
ASIOGetBufferSize() min=64 max=4096 preferred=4096 granularity=-1
ASIOSetSampleRate(sampleRate=192000.000000)
ASIOOutputReady() Supported
i= 0 ch= 0 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 1
i= 1 ch= 1 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 2
i= 2 ch= 2 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 3
i= 3 ch= 3 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 4
i= 4 ch= 4 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 5
i= 5 ch= 5 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 6
i= 6 ch= 6 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 7
i= 7 ch= 7 isInput=1 chGroup=00000000 type=18 name=FW 2626 Analog In 8
i= 8 ch= 8 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 1
i= 9 ch= 9 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 2
i=10 ch=10 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 3
i=11 ch=11 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 4
i=12 ch=12 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 5
i=13 ch=13 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 6
i=14 ch=14 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 7
i=15 ch=15 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT A In 8
i=16 ch=16 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 1
i=17 ch=17 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 2
i=18 ch=18 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 3
i=19 ch=19 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 4
i=20 ch=20 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 5
i=21 ch=21 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 6
i=22 ch=22 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 7
i=23 ch=23 isInput=1 chGroup=00000000 type=18 name=FW 2626 ADAT B In 8
i=24 ch=24 isInput=1 chGroup=00000000 type=18 name=FW 2626 SPDIF In L
i=25 ch=25 isInput=1 chGroup=00000000 type=18 name=FW 2626 SPDIF In R
i=26 ch= 0 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 1
i=27 ch= 1 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 2
i=28 ch= 2 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 3
i=29 ch= 3 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 4
i=30 ch= 4 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 5
i=31 ch= 5 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 6
i=32 ch= 6 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 7
i=33 ch= 7 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 8
i=34 ch= 8 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 9
i=35 ch= 9 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 10
i=36 ch=10 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 11
i=37 ch=11 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 12
i=38 ch=12 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 13
i=39 ch=13 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 14
i=40 ch=14 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 15
i=41 ch=15 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 16
i=42 ch=16 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 17
i=43 ch=17 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 18
i=44 ch=18 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 19
i=45 ch=19 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 20
i=46 ch=20 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 21
i=47 ch=21 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 22
i=48 ch=22 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 23
i=49 ch=23 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 24
i=50 ch=24 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 25
i=51 ch=25 isInput=0 chGroup=00000000 type=18 name=FW 2626 SW Return 26
ASIOGetLatencies() input=4271 output=4271
ASIOGetClockSources() result=0 numOfClockSources=1
 idx=0 assocCh=-1 assocGrp=-1 current=1 name=See Control Panel
```

# ASIO4ALL #

```
  asioVersion:   0
  driverVersion: 2
  Name:          ASIO4ALL v2
ASIOGetChannels() inputs=2 outputs=2
ASIOGetBufferSize() min=64 max=2048 preferred=256 granularity=8
ASIOSetSampleRate(sampleRate=192000.000000)
ASIOOutputReady() Not supported
i= 0 ch= 0 isInput=1 chGroup=00000000 type=18 name=Not Connected 1
i= 1 ch= 1 isInput=1 chGroup=00000000 type=18 name=Not Connected 2
i= 2 ch= 0 isInput=0 chGroup=00000000 type=18 name=Not Connected 1
i= 3 ch= 1 isInput=0 chGroup=00000000 type=18 name=Not Connected 2
ASIOGetLatencies() input=256 output=256
ASIOGetClockSources() result=0 numOfClockSources=1
 idx=0 assocCh=0 assocGrp=0 current=1 name=Big Ben
```