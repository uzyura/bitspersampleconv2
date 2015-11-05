Pulse5はASIOデバイスから5波長分の長さの正弦波のパルスを22.5Hz～18286Hzまでの1/3oct刻みで順に出力し、
同時にASIOデバイスから録音するプログラムです。

スピーカーのパルス応答の具合を観察するために作成しました。
ASIOデバイスは96kHz 24bit録音再生対応である必要があります。

ASIO4ALLでは駄目で本物のASIO2デバイスでないとうまく動作しないと思います。

# 動作確認デバイス #

Version 1.0.8
  * M-AUDIO ProFire 2626
  * Realtek ASIO

Version 1.0.3
  * Creative X-Fi titanium PCI-e　オーディオクリエイションモード、マスタークロック96kHz、ビットマッチプレイバック、ビットマッチレコーディング
  * M-AUDIO ProFire 2626

# インストール #

予め以下の場所からVisual C++ 2010ランタイムライブラリをダウンロードしてインストールして下さい:

32ビットWindows用:
http://www.microsoft.com/downloads/details.aspx?familyid=a7b7a05e-6de6-4d3a-a423-37bf0912db84&displaylang=ja

64ビットWindows用:
http://www.microsoft.com/downloads/details.aspx?familyid=bd512d9e-43c8-4655-81bf-9350143d5867&displaylang=ja

その後、Pulse5を以下の場所よりダウンロードし、中に入っているsetup.exeをダブルクリックしてインストールを行います。

32ビットWindows用:
http://bitspersampleconv2.googlecode.com/files/Pulse5x86Setup10008.zip

64ビットWindows用:
http://bitspersampleconv2.googlecode.com/files/Pulse5x64Setup10008.zip


# 使用方法 #

ASIOデバイス自体の性能を測るときは入力端子と出力端子をケーブルでつなげます。

スピーカーの性能を測るときはASIOデバイスの出力にスピーカーをつなげ、
マイク入力に測定用マイクをつなげます。

Pulse5を起動し、画面のグループボックス1～4まで設定を行ってStartを押すと再生と録音が始まります。

1個しかASIOデバイスがない場合は自動でそのデバイスのドライバがロードされるので
1のASIOドライバ選択を飛ばして2から設定してください。

2のPulse Countは何波長分のパルスを出力するか設定します。デフォルトは5波長出力です。

3は入出力デバイス選択

出力デバイスは、複数選択可です。CTRLを押しながら選択すると複数選択できます。
入力デバイスは1個のみ選択可です。

4はクロックソース選択

5は出力ファイル名選択

3～5まで行ったらStartを押します

![http://bitspersampleconv2.googlecode.com/files/Pulse5_10005.png](http://bitspersampleconv2.googlecode.com/files/Pulse5_10005.png)

# ソースコードからexeを作る方法 #

Visual Studio 2010が必要です

http://www.steinberg.net/en/company/3rd_party_developer.html
からASIO SDKをダウンロードします(無料)

TortoiseSVNなどを使って
https://bitspersampleconv2.googlecode.com/svn/trunk/
からPulse5のソースコードをダウンロード(チェックアウト)します

チェックアウトしてきたツリーにASIO SDKをunzipしたものをコピーして
同じ親ディレクトリのなかにASIOSDK2とAsioIOがある状態にして
AsioIO\AsioIODLL\copyasiofiles.batを実行するとASIOSDKの中から
asio.cppなど9つのファイルをAsioIO\AsioIODLL\にコピーします

Visual Studio 2010でPulse5.slnを開いてF5でビルドして実行します

C#側からC++側に再生するデータを再生開始前に全部送るつくりになっています。
このためASIOデバイスからやってくるbufferSwitchコールバックがC++内で処理され、
C#まで伝わりません。
この方式のメリットとしては低性能なPCで低レイテンシに設定しても比較的音が途切れないのではないかと思います。
デメリットは、バッファ処理のプログラムをC++側に書かなくてはならないので面倒くさい点でしょうか。C#の関数をC++から呼ぶ方法を調べるのが面倒でこのような役割分担にしましたがかえって面倒くさくなったような気がします。

鳴る音の周波数(Hz)

22.5
28.3
35.7
45
56.7
71.4
90
113
142
180
226
285
359
453
571
720
907
1143
1440
1814
2285
2880
3628
4571
5760
7257
9143
11520
14514
18286

この数列は440Hzのラの音を聴くために2で割っていって設計したのですが55÷2＝22.5という残念な計算間違いをしたため440Hzの音が鳴らない数列になってしまいました。wwww


ASIO is a trademark and software of Steinberg Media Technologies GmbH