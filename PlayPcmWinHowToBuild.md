# How to build PlayPcmWin #

## Prerequisites ##

  * Windows 7 x64
  * Visual Studio 2010 Professional
  * Expression Blend 4 (I have) or Microsoft Expression Blend SDK for Silverlight 4

## 0. Download and install Windows Driver Kit 7.1.0 ##

Install Windows Driver Kit 7.1.0 and update Visual Studio user library path
```
i386:  $(LibraryPath);C:\WinDDK\7600.16385.1\lib\win7\i386
amd64: $(LibraryPath);C:\WinDDK\7600.16385.1\lib\win7\amd64
```
respectively.

## 1. Download PlayPcmWin sourcecode ##

Checkout PlayPcmWin sourcecode onto **C:\work\BpsConvWin2**. For example if you use TortoiseSVN, open windows explorer, create **C:\work\BpsConvWin2** folder, right-click the BpsConvWin2 folder and `[`SVN Checkout...`]` to open Checkout dialog. Enter the following URL on `[`URL of repository`]`

http://bitspersampleconv2.googlecode.com/svn/trunk

## 2. Create keypair using sn.exe ##

open Windows SDK command prompt and enter following commands

```
> cd \work\BpsConvWin2
C:\work\BpsConvWin2> sn -k CueSheetReaderTest\CueSheetReaderTest.snk
C:\work\BpsConvWin2> sn -k FlacDecodeCS\FlacDecodeCS.snk
C:\work\BpsConvWin2> sn -k FlacDecodeCSTest\FlacDecodeCSTest.snk
C:\work\BpsConvWin2> sn -k PcmDataLib\PcmDataLib.snk
C:\work\BpsConvWin2> sn -k PlayPcmWin\PlayPcmWin.snk
C:\work\BpsConvWin2> sn -k PlayPcmWinTestBench\PlayPcmWinTestBench.snk
C:\work\BpsConvWin2> sn -k WasapiCS\WasapiCS.snk
C:\work\BpsConvWin2> sn -k WasapiPcmUtil\WasapiPcmUtil.snk
C:\work\BpsConvWin2> sn -k WavRWLib2\WavRWLib2.snk
C:\work\BpsConvWin2> sn -k WavRWTest\WavRWTest.snk
C:\work\BpsConvWin2> sn -k WWDirectComputeCS\WWDirectComputeCS.snk
C:\work\BpsConvWin2> sn -k WWXmlRW\WWXmlRW.snk
C:\work\BpsConvWin2> sn -k RecPcmWin\RecPcmWin.snk
```

## 3. Prepare libFLAC\_static.lib and libogg\_static.lib ##

Download libogg-x.y.z.tar.gz source code from the following site:
http://xiph.org/downloads/

Extract onto C:\work folder. Open C:\work\libogg-x.y.z\win32\VS2010\libogg\_static.sln and release-build libogg\_static.lib

Download flac-s.t.u.tar.gz source code from the following site:
http://flac.sourceforge.net/download.html

Extract it onto C:\work folder. Open C:\work\flac-s.t.u\FLAC.sln. select release build. right-click libFLAC\_static on the solution explorer and `[`Property`]` to open libFLAC\_static property page. on the page, Select `[`VC++ directories`]``[`include directories`]` and append **C:\work\libogg-x.y.z\include;**

Right-click  `[`libFLAC\_static`]` and select `[`Build`]` to build libFLAC\_static.lib

In order to build FLAC, I suppose, the Netwide assembler is needed additionally. It can be downloaded from the following site:
http://sourceforge.net/projects/nasm/

Only Win32 build of libFLAC and libogg are needed because PlayPcmWin creates dedicated FLAC decode process that runs on 32bit mode.

## 4. Download and install DirectX SDK ##

Download and install DirectX SDK.

Latest DirectSDK is available here:
http://www.microsoft.com/download/en/details.aspx?displaylang=en&id=6812

## 5. Build PlayPcmWin ##

Open C:\work\BpsConvWin2\PlayPcmWin\PlayPcmWin.sln

Select build target (Release x64 or Release x86. Other platform such as AnyCPU doesn't work). Right-click PlayPcmWin project on the solution explorer window and flag it as startup project and build the solution.

I suppose the folder path to libFLAC\_static.lib and libogg\_static.lib must be specified somewhere to build PlayPcmWin successfully. Please play it by ear if any problem arises.