# RF64 WAVファイルフォーマットについて #

RIFF WAVEファイル形式は、素の状態では、RIFFチャンクやDATAチャンクのバイト数を表す
chunkSizeが32ビット整数のため、4GBを超えるRIFFチャンクサイズ、DATAチャンクサイズを表現することが出来ない。

RF64形式は、4GBを超えるサイズのDATAチャンクを持つことができる

# 実際の出力を観察する #

Adobe Audition CS 5.5で出力したRF64形式のWAVファイルのヘッダ部分

  * 1時間18分28秒のCDをリッピングして作ったWAVファイルをAuditionで読み込み192kHz 24bitにアップサンプルしてRF64形式 WAVで保存。
  * このファイルのファイルサイズは 0x14355d494 (5424665748)バイト

```
---------|-0--1--2--3--4--5--6--7--8--9--A--B--C--D--E--F-|---0123456789ABCDEF
000000000|52 46 36 34 ff ff ff ff 57 41 56 45 64 73 36 34 |   RF64....WAVEds64
000000010|1c 00 00 00 8c d4 55 43 01 00 00 00 00 c0 55 43 |   ....菰UC.....ﾀUC
000000020|01 00 00 00 00 a0 e3 35 00 00 00 00 00 00 00 00 |   ......5........
000000030|66 6d 74 20 12 00 00 00 01 00 02 00 00 ee 02 00 |   fmt ............
000000040|00 94 11 00 06 00 18 00 00 00 64 61 74 61 ff ff |   ..........data..
000000050|ff ff 以降PCMデータが続く                       |   ..              
---------|------------------------------------------------|-------------------
```
↑表1: ヘッダ部分のバイナリダンプ

|オフセット|サイズ|値 カッコ内は10進数表記   | 内容                   |
|:----|:--|:---------------|:---------------------|
|0         |    4 | 'RF64'                   | RF64 chunkId           |
|4         |    4 | 0xffffffff = ds64.riffSizeを参照 | RF64 chunkSize         |
|8         |    4 | 'WAVE'                   | RF64 rf64Type          |
|0xc (12)  |    4 | 'ds64'                   | DataSize64 chunkId     |
|0x10 (16) |    4 | 0x1c (28)                | DataSize64 chunkSize   |
|0x14 (20) |    8 | 0x14355d48c (5424665740) | DataSize64 riffSize    |
|0x1c (28) |    8 | 0x14355c000 (5424660480) | DataSize64 dataSize    |
|0x24 (36) |    8 | 0x35e3a000 (904110080) ※1  | DataSize64 sampleCount |
|0x2c (44) |    4 | 0                        | DataSize64 tableLength |
|0x30 (48) |    4 | 'fmt '                   | Format chunkId         |
|0x34 (52) |    4 | 0x12 (18)                | Format chunkSize       |
|0x38 (56) |    2 | 1 = WAVE\_FORMAT\_PCM      | Format formatType      |
|0x3a (58) |    2 | 2 = 2ch stereo           | Format channelCount    |
|0x3c (60) |    4 | 0x2ee00 (192000) = 192000 Hz      | Format sampleRate      |
|0x40 (64) |    4 | 0x119400 (1152000) = 192000Hz × 2ch × 24bit ÷ 8 | Format bytesPerSecond   |
|0x44 (68) |    2 | 6 = 6 bytes / sample      | Format blockAlignment     |
|0x46 (70) |    2 | 0x18 (24) = 量子化ビット数24bit | Format bitsPerSample     |
|0x48 (72) |    2 | 0  extra data なし        | Format cbSize        |
|0x4a (74) |    4 | 'data'                    | Data chunkId         |
|0x4e (78) |    4 | 0xffffffff = ds64.dataSizeを参照 | Data chunkSize       |
|0x52 (82) |   0x14355c000 | PCMデータ         | Data waveData        |
|0x14355c052 |   4 | '＿PMX'         | XMPメタデータ chunkId     |
|0x14355c056 |   4 | 0x1439(5177)   | XMPメタデータ chunkSize     |
|0x14355c05a |   0x1439 ※2 | XMPメタデータのXML   | XMPメタデータ 内容          |
|0x14355d493 |   1 ※2 | 0 = XMPメタデータのXMLのpad | pad                  |

↑表2: 表1のバイナリデータの意味

  * ※1… 1サンプルあたり6バイトなので、DataSize64.sampleCountを6倍するとDataSize64.dataSizeになる。
  * ※2… 一般に、あらゆるWAVファイルのチャンクはchunkSizeが奇数の場合そのチャンクの終わりに1バイトの0 (パッド)を連結する。

# RF64形式のWAVファイルを出力する #

  * ファイルの先頭には、'RIFF'の代わりに、'RF64'と書きこむ。
  * RIFF chunkSizeには0xffffffffを書き込む。
  * RIFFチャンクのすぐ後にDS64チャンクを書き込む。
  * DS64チャンクには、riffSize(ファイルサイズ - 8)、dataSize(PCMデータのバイト数)、sampleCount(PCMサンプルの総数。24ビットステレオの場合dataSize÷6)、tableLength 0を書き込む。
  * Formatチャンクはいつも通り書きこむ。
  * dataチャンクのchunkSizeには0xffffffffを書き込む。
  * Adobe AuditionはDATAチャンクの後に長大なXMPメタデータを付けてくるが、必須ではないと思う。

# 仕様書 #

http://tech.ebu.ch/docs/tech/tech3306-2009.pdf

# Adobe AuditionがRF64保存に切り替えるタイミングについて #

Adobe Auditionは、WAV保存する場合、4GBを超えると自動でRF64形式保存に切り替わる。

DATAチャンクよりも先にRIFFチャンクが0xffffffffを超える。

通常のWAV形式保存からRF64形式保存に移行するトリガーは、RIFFチャンクが0xffffffffを超えるタイミングか、それよりもちょっと手前となるだろう。
実際何バイトでRF64形式に切り替えるかは調べる価値があるだろう(未調査)