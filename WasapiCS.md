WasapiCSはC#からWASAPIを使うためのクラスです。

C#のクラスファイルWasapiCS.csとC++で書かれたWasapiIODLL.DLLから構成されます。

# ソースコード #

  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/WasapiCS
  * http://code.google.com/p/bitspersampleconv2/source/browse/#svn/trunk/WasapiIODLL
svnでチェックアウトします。

# サンプルプログラム #

  * PlayPcmWin WasapiCSを使用してWAVファイルを再生するサンプル
  * RecPcmWin WasapiCSを使用してWAVファイルを録音するサンプル

# 使い方 #

Visual Studio 2010でC#プログラムの新規ソリューションを作成し
WasapiCS.csprojとWasapiIODLL.vcxprojを追加します。
その後、以下のようにするとWASAPI排他モードのイベント駆動モードでPCMデータを再生できます。

```
using Wasapiex;

// 初期化
WasapiCS wasapi = new WasapiCS();
int hr = wasapi.Init();

// デバイス一覧取得
hr = wasapi.DoDeviceEnumeration(WasapiCS.DeviceType.Play);
int nDevices = wasapi.GetDeviceCount();
for (int i = 0; i < nDevices; ++i) {
    Console.WriteLine("{0}: {1}", i, wasapi.GetDeviceName(i));
}

// 使用デバイス選択
hr = wasapi.ChooseDevice(0);
hr = wasapi.Setup(WasapiCS.DataFeedMode.EventDriven,
        44100, 16, 200);

// 出力するPCMデータをセット
byte[] outputPcm = new byte[44100 * 2 * 2 * 10]; // 44100Hz 16bit 2ch 10秒
wasapi.SetOutputData(outputPcm);

// 再生開始
hr = wasapi.Start();
while (!wasapi.Run(1000)) {
    System.Threading.Thread.Sleep(1);
    Console.WriteLine("...");
}

// 再生終了
wasapi.Stop();

// デバイス選択解除
wasapi.Unsetup();

// 終了
wasapi.Term();
wasapi = null;
```