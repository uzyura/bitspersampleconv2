WASAPI排他モードは、Windows Vista以降で使える(Windows7推奨)
高音質低レイテンシのPCM音声再生・録音APIです。

# WASAPI排他モードの実験プログラム #
  * コンソールで再生するシンプルな実験プログラム [PlayPcm](http://code.google.com/p/bitspersampleconv2/source/browse/#svn%2Ftrunk%2FPlayPcm)
  * GUI付き再生プログラム PlayPcmWin
  * GUI付き録音プログラム RecPcmWin
  * C#からWASAPIを使うためのクラスWasapiCSについて [WasapiCS](http://code.google.com/p/bitspersampleconv2/wiki/WasapiCS)
  * 再生したデータを録音して再生したデータと録音したデータが一致するかどうか確認するプログラム　[WasapiBitmatchChecker](WasapiBitmatchChecker.md)
  * サンプルレートの変換にはResampler MFTを使用します。 [HowToUseResamplerMFT](HowToUseResamplerMFT.md)

# WASAPIの音楽再生機能を100%楽しむための、おすすめオーディオインターフェース #
## Musiland Monitor 02 US Dragon ##

あらゆるPCMフォーマットの動作テストが可能です。
DoP DSD再生には対応していません。

## Lynx L22 ##

Lynx L22は、アナログ音声出力は44.1kHz～192kHz、16ビット、24ビットの全ての組み合わせがWASAPI排他イベント駆動モードで使用可能です。

デジタル出力(Lynx L22のデフォルトのルーティング状態では、PlayPcmWinでは、出力7/8と表示されます)は、デジタル出力モードをS/PDIF出力にすると、44.1kHz～96kHz、16ビット、24ビットの全ての組み合わせがWASAPI排他イベント駆動モードで使用可能。176.4kHzと192kHzは出ません。44.1kHz/16bitと96kHz/16bitで、L22デジタル出力→L22デジタル入力で、bit perfect再生できていることを確認。

ワードクロック入出力端子を備えており、遊べます。

Lynx AES16は、PCに取り付ける前に説明書を読んでジャンパーの設定を行いましょう

Version 2.0 Build 19以降のドライバはWaveRTです。

[Issue 41](https://code.google.com/p/bitspersampleconv2/issues/detail?id=41) もご覧ください。

なお、ドライバのリリースノートに以下のようなことが書いてあります。
あらゆるオーディオインターフェースに言えることです。
(http://www.lynxstudio.com/pop/support_downloads_notes.asp?i=82)
より引用:

PLEASE NOTE: For Windows Vista and Windows 7, Lynx recommends disabling SpeedStep in your BIOS, and setting the Power Options in the OS to "High Performance".  This can improve the overall reliability of audio playback in MME/DirectSound applications.

## Creative X-Fi Titanium HD ##

アナログ音声出力は、44.1kHzと88.2kHzは、WASAPI排他タイマー駆動モードでしか動作しないようですが、
他のサンプリング周波数(48kHz、96kHz, 176.4kHz 192kHz)ではWASAPI排他イベント駆動モードで動作可能。

デジタル出力はS/PDIF光出力のみで、96kHzまでの対応です。

ワードクロック入出力端子はありません。

WaveRTらしいです。(未確認)

# デバイスごとの使用可能なPCMデータフォーマット一覧 (再生の場合) #

使用可能なフォーマット(サンプリング周波数と量子化ビット数の組み合わせ)は
デバイスごとに異なります。

以下の表でOKと書いてあっても、使えなかったり機能に制限がある場合があります。
  * Realtek ALC889Aは、192kHz/24bit再生する場合は、出力レイテンシーを170ms以下に設定する必要があるようです。Realtekドライバをアンインストールして、Windows標準ドライバを入れると、使えるフォーマット(88.2kHz、~~176.4kHz~~)が増えるらしいです。
  * Creative X-FiTiHDの44.1kHzと88.2kHzは、イベント駆動モードで動作できない。タイマー駆動モードならば動作する。
  * Creative X-FiTiHDはS/PDIF出力から量子化ビット数24ビット出力が可能です。(44.1kHz 24bitと48kHz 24bitについて確認)
  * Creative X-FiTiHDのWASAPI共有モードのサンプルレート設定は、[コントロールパネル][サウンド][再生]ではなく、Creativeコンソールランチャで設定した値が使用されます。(?)
  * RME Fireface400とM-AUDIO ProFire2626は、機器に設定されているマスターサンプリングレートのみ使用できる。マスターサンプリングレートはコントロールパネルで変更できる。ASIOではマスターサンプリングレート以外のサンプリング周波数に設定可能。
  * M-AUDIO ProFire 2626のS/PDIF出力は、量子化ビット数24ビット出力の場合にコピープロテクトがかかるようです。
  * ~~RME Fireface400は、ASIOでは量子化ビット数24ビットが使用できるがWASAPIでは選択できない。~~ PlayPcmWinのバグでした。[Issue 16](https://code.google.com/p/bitspersampleconv2/issues/detail?id=16)

  * Prodigy HD2はS/PDIF出力に量子化ビット数32ビットのデータを入れると出力に量子化ビット数16ビットのデータが出てきます。
  * Lynx L22は、アナログ音声出力は44.1kHz～192kHzの範囲のデータが再生出来ます。デジタル出力は44.1kHz～96kHzの範囲のデータが出力できます。
  * Halide bridgeは、[Sint24に固定する]を選択すると良いそうです。
  * USB Sound Blaster Digital Music Premium HDは、タイマー駆動再生よりもイベント駆動再生のほうが安定動作します。
  * 量子化ビット数32ビット(i32V32)がWASAPI Setupできても、DACの入力まで32ビットのデータが渡るとは限りません。24ビットDAC搭載機種で32ビットのSetupが出来る機種があります。
  * S/PDIF出力は量子化ビット数24ビットまでの規格であり量子化ビット数32ビットのデータは通りません。

# 表1: オーディオインターフェースごとの、使用可能なフォーマット一覧 #


## スピーカー (Realtek High Definition Audio) (Realtek ALC889A Realtekドライバ) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | NA          | OK          | NA          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | NA          | OK          | NA          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (Creative SB X-Fi) (Creative X-Fi Titanium HD SB-XFT-HD) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (Prodigy HD2 Audio) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## Multichannel (M-Audio ProFire 2626) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| NA          | NA          | NA          | NA          | NA          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (RME Fireface 400) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| NA          | NA          | NA          | NA          | NA          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## デジタル出力 (E-MU 0404 | USB) ##
|16/44|16/48|16/88|16/96|16/176|16/192|
|:----|:----|:----|:----|:-----|:-----|
|OK   |OK   |OK   |OK   |OK    |OK    |
|32/44|32/48|32/88|32/96|32/176|32/192|
|NA   |NA   |NA   |NA   |NA    |NA    |

## スピーカー (2- Lynx L22) (アナログ音声出力) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (USB Sound Blaster HD) (USB Sound Blaster Digital Music Premium HD SB-DM-PHD アナログ音声出力) ※上記説明参照 ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| NA          | OK          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | OK          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## デジタル オーディオ インターフェイス (USB Sound Blaster HD) (USB Sound Blaster Digital Music Premium HD SB-DM-PHD S/PDIF出力) ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| OK          | OK          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (USB Sound Device        ) (響音DIGI+ SD-U1SOUND-T4 C-Media 6206 マイクロソフト標準USBオーディオドライバ)※標準ドライバだとS/PDIFから音が出ません ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## 響音DIGI+ SD-U1SOUND-T4 C-Media 6206 C-Mediaドライバ S/PDIF出力 ##
|16/44|16/48|16/88|16/96|16/176|16/192|
|:----|:----|:----|:----|:-----|:-----|
|NA   |OK   |NA   |NA   |NA    |NA    |
|32/44|32/48|32/88|32/96|32/176|32/192|
|NA   |NA   |NA   |NA   |NA    |NA    |

## スピーカー (Lynx AES16e) ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## スピーカー (Cirrus Logic CS4206A (AB 03)) Mac Mini(Mid 2010) ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## デジタル オーディオ (S/PDIF) (Cirrus Logic CS4206A (AB 03)) Mac Mini (Mid 2010) ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

## SPDIF インターフェイス (FOSTEX HP-A3) ##
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| NA          | NA          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| NA          | NA          | NA          | OK          | NA          | NA          | NA          | NA          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| NA          | NA          | NA          | NA          | NA          | NA          | NA          | NA          |

スピーカー (MUSILAND Monitor 02 US Dragon)
| 44kHz i16V16| 48kHz i16V16| 88kHz i16V16| 96kHz i16V16|176kHz i16V16|192kHz i16V16|352kHz i16V16|384kHz i16V16|
|:------------|:------------|:------------|:------------|:------------|:------------|:------------|:------------|
| OK          | OK          | OK          | OK          | OK          | OK          | OK          | OK          |
| 44kHz i24V24| 48kHz i24V24| 88kHz i24V24| 96kHz i24V24|176kHz i24V24|192kHz i24V24|352kHz i24V24|384kHz i24V24|
| OK          | OK          | OK          | OK          | OK          | OK          | OK          | OK          |
| 44kHz i32V24| 48kHz i32V24| 88kHz i32V24| 96kHz i32V24|176kHz i32V24|192kHz i32V24|352kHz i32V24|384kHz i32V24|
| OK          | OK          | OK          | OK          | OK          | OK          | OK          | OK          |
| 44kHz i32V32| 48kHz i32V32| 88kHz i32V32| 96kHz i32V32|176kHz i32V32|192kHz i32V32|352kHz i32V32|384kHz i32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | OK          | OK          |
| 44kHz f32V32| 48kHz f32V32| 88kHz f32V32| 96kHz f32V32|176kHz f32V32|192kHz f32V32|352kHz f32V32|384kHz f32V32|
| OK          | OK          | OK          | OK          | OK          | OK          | OK          | OK          |

# 資料 #

  * 共有モードの再生 http://msdn.microsoft.com/en-us/library/dd316756%28v=VS.85%29.aspx
  * 共有モードの録音 http://msdn.microsoft.com/en-us/library/dd370800%28v=VS.85%29.aspx
  * 排他モードについての資料 http://msdn.microsoft.com/en-us/library/dd370844%28v=VS.85%29.aspx
  * サンプルプログラム、バグ情報等 http://blogs.msdn.com/b/matthew_van_eerde/archive/tags/audio/

# Windows 7で追加された(Windows Vistaで使えない)機能について #

  * 録音時に、音切れが発生したかどうかを知る機能 AUDCLNT\_BUFFERFLAGS\_DATA\_DISCONTINUITY
  * S/PDIFのコピー禁止フラグを立てる機能
  * HDMIのHDCPを有効にする機能
  * デジタル出力を無効にする機能