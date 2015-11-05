# How to build [WWAudioFilter](WWAudioFilter.md) #

## Prerequisites ##

  * Windows 7 x64
  * Visual Studio 2010 Professional

## 1. Download WWAudioFilter sourcecode ##

Checkout WWAudioFilter sourcecode onto **C:\work\BpsConvWin2**. For example if you use TortoiseSVN, open windows explorer, create **C:\work\BpsConvWin2** folder, right-click the BpsConvWin2 folder and `[`SVN Checkout...`]` to open Checkout dialog. Enter the following URL on `[`URL of repository`]`

http://bitspersampleconv2.googlecode.com/svn/trunk

## 2. Create keypair using sn.exe ##

open Windows SDK command prompt and enter following commands

```
> cd \work\BpsConvWin2
C:\work\BpsConvWin2> sn -k WWAudioFilter\WWAudioFilter.snk
C:\work\BpsConvWin2> sn -k WWFlacRWCS\WWFlacRWCS.snk
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

Only the x64 build of libFLAC and libogg are needed.

## 4. Build WWAudioFilter ##

Open C:\work\BpsConvWin2\WWAudioFilter\WWAudioFilter.sln

Set build target to Release x64. Other platforms such as AnyCPU or x86 doesn't work! Right-click WWAudioFilter project on the solution explorer window and flag it as startup project and build the solution.

Folder path to libFLAC\_static.lib and libogg\_static.lib must be specified to WWAudioFilter project include path and library path to build WWAudioFilter successfully.