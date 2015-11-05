# PlayPcmWin Localization #

## Localization file ##

http://bitspersampleconv2.googlecode.com/files/PlayPcmWinLocalize464.txt

## Main window ##

![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMain.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMain.png)

![http://bitspersampleconv2.googlecode.com/files/ppwLocalizePlaymode.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizePlaymode.png) ![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuFile.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuFile.png) ![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuTools.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuTools.png) ![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuPlaylist.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuPlaylist.png) ![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuHelp.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeMenuHelp.png)


|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
|(1) | MainGroupBoxPlaylist	        | Playlist(Drop audio files from Explorer to add files) |         |
|(2) | MainDataGridColumnTitle	     | Title        | track title / song name |
|(3) | MainDataGridColumnDuration	  | Duration     | track duration |
|(4) | MainDataGridColumnArtist	    | Artists      | artist name |
|(5) | MainDataGridColumnAlbumTitle	 | Album name   |         |
|(6) | MainDataGridColumnSampleRate	 | Sample rate  |         |
|(7) | MainDataGridColumnQuantizationBitRate	 | Quantization bit rate |         |
|(8) | MainDataGridColumnNumChannels	 | Number of channels | usually 2 channels == LR stereo |
|(9) | MainDataGridColumnBitRate	   | Bitrate      | uncompressed CD == 1411 kbps |
|~~(10)~~ | MainDataGridColumnIndexNr	   | Index        | CD Index number in CUE sheet. not need to translate |
|(11) | MainDataGridColumnReadSeparaterAfter	 | Batch read endpoint | read PCM data onto main memory until the checked track. |
|(12) | MainButtonClearPlayList	     | `_Clear the playlist` | `underbar _ ` means keyboard accelerator alphabet. Keyboard accelerator alphabet is C. see next comment. |
|(13) | MainButtonDelistSelected	    | `Delist the selected file(_R)` | `(_R)` means keyboard accelerator alphabet is R. Press ALT key to display underbar |
|(14) | MainGroupBoxPlaybackControl	 | Playback control |         |
|(15) | MainButtonPlay	              | `_Play`      |         |
|(16) | MainButtonStop	              | `_Stop`      |         |
|(17-1) | MainButtonPause	             | `Pa_use`     | shortcut key is U |
|(17-2) | MainButtonResume	            | `Resume(_U)` | shortcut key is U. This text is displayed while pausing |
|(18) | MainButtonPrev	              | Prev         |         |
|(19) | MainButtonNext	              | Next         |         |
|(20) | -                            | -            | play mode. see (36) to (41) |
|(21) | MainExpanderSettings	        | Settings     |         |
|(22) | MainGroupBoxWasapiSettings	  | WASAPI settings |         |
|(23) | MainGroupBoxWasapiOperationMode	 | Operation mode |         |
|(24) | MainRadioButtonExclusive	    | Exclusive    |         |
|(25) | MainRadioButtonShared	       | Shared       |         |
|(26) | MainGroupBoxWasapiDataFeedMode	 | Data feed mode |         |
|(27) | MainRadioButtonEventDriven	  | Event driven |         |
|(28) | MainRadioButtonTimerDriven	  | Timer driven |         |
|(29) | MainGroupBoxWasapiOutputLatency	 | Output latency |         |
|(30) | -                            | -            | not need to localize |
|(31) | MainGroupBoxOutputDevices	   | Output device |         |
|(32) | MainButtonSettings	          | `_Detailed settings...` | ... means it needs additional user interaction to perform command |
|(33) | MainButtonInspectDevice	     | `L_ist supported formats` |         |
|(34) | MainGroupBoxLog	             | Log          |         |
|(35) | -                            | -            | Status bar text. see below |
|(36) | MainPlayModeAllTracks	       | All tracks   |         |
|(37) | MainPlayModeAllTracksRepeat	 | All tracks repeat |         |
|(38) | MainPlayModeOneTrack	        | One track    |         |
|(39) | MainPlayModeOneTrackRepeat	  | One track repeat |         |
|(40) | MainPlayModeShuffle	         | Shuffle      |         |
|(41) | MainPlayModeShuffleRepeat	   | Shuffle repeat |         |
|(42) | MenuFile	                    | `_File`      |         |
|(43) | MenuItemFileNew	             | `Clear the playlist(_N)` |         |
|(44) | MenuItemFileOpen	            | `_Open...`   |         |
|(45) | MenuItemFileSaveCueAs	       | `Save the playlist as a _CUE sheet...` |         |
|(46) | MenuItemFileSaveAs	          | `Save _As...` |         |
|(47) | MenuItemFileExit	            | `E_xit`      |         |
|(48) | MenuTool	                    | `_Tools`     |         |
|(49) | MenuItemToolSettings	        | `_Settings`  |         |
|(50) | MenuPlayList	                | `P_laylist`  |         |
|(51) | MenuItemPlayListClear	       | `_Clear the playlist` |         |
|(52) | MenuItemPlayListItemEditMode	 | `Playlist item edit mode(_E)` |         |
|(53) | MenuHelp	                    | `_Help`      |         |
|(54) | MenuItemHelpAbout	           | `About(_V)`  |         |
|(55) | MenuItemHelpWeb	             | `Visit PlayPcmWin _Website` |         |

## Status bar texts ##
|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
|(35-1) | MainStatusChangingPlayGroup	 | Reading files... |         |
|(35-2) | MainStatusPaused	            | Paused.      |         |
|(35-3) | MainStatusPlaying	           | Playing.     |         |
|(35-4) | MainStatusPleaseCreatePlaylist	 | Please create the playlist. |         |
|(35-5) | MainStatusPressPlayButton	   | Add more files to the playlist or Press play button |         |
|(35-6) | MainStatusReadCompleted	     | Read completed. ready to play. |         |
|(35-7) | MainStatusReadingFiles	      | Setup device completed. Now reading files to Main memory... |         |
|(35-8) | MainStatusStopping	          | Stopping...  |         |
|(35-9) | MainStatusReadingPlaylist    | Reading playlist... |         |

## Error messages ##
|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
|(E-1) | MemoryExhausted	             | Memory exhausted. Please reduce files on playlist. |         |
|(E-2) | CannotAddFile	               | Sorry... Please stop playback to add files to the playlist. |         |
|(E-3) | MessageNothingToStore	       | Nothing to store  | attempt to save playlist but there is nothing |
|(E-4) | NotSupportedFileFormat	      | Not supported file format |         |
|(E-5) | NotSupportedQuantizationBitRate	 | Not supported quantization bit rate. supported formats are 16, 24 and 32 |         |
|(E-6) | PlayStartFailed	             | Play start failed |         |
|(E-7) | ReadError	                   | Read error   | means file read error |
|(E-8) | ReadFailedFiles	             | Read failed files | used when failed to read several files |
|(E-9) | ReadFileFailed	              | Read file failed `{0}` | `{0}` is replaced with file name |
|(E-10)| RestoreFailedFiles	          | Missing files while restoring playlist |         |
|(E-11)| SaveFileFailed	              | Save file failed |         |
|(E-12)| TooManyChannels	             | This file contains Too many channels(>31) |         |
|(E-13)| UnexpectedEndOfStream	       | Unexpected end of stream. | stream means file :) |
|(E-14)| MD5SumMismatch               | MD5 sum validation failed! File may be corrupted: {0} MD5 in metadata = {1} MD5 from PCM data= {2} |         |
|(E-15)| MD5SumValid                  | MD5sum validation succeeded. {0} | {0} is replaced with FLAC file |
|(E-16)| DroppedDataIsNotFile         | Some kind of data is dropped but the data is not file nor folder. Failed to process dropped items. |         |

## Log messages ##
|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
| (Log-1) | ClippedSampleDetected	       | Clipped sample value detected on floating point PCM data! Clipped sample value count=`{1}`, FileName=`{0}` | `{0}` is replaced with filename. `{1}` is replaced with clipped sample count. |
| (Log-2) | DeviceStateChanged	          | DeviceStateChanged. DeviceId=`{0}` | `{0}` is replaced with device name |
| (Log-3) | PlayCompletedElapsedTimeIs	  | Play completed. elapsed time is `{0}`ms|         |
| (Log-4) | ReadPlayGroupNCompleted	     | Read playgroup `{0}` completed. Elapsed time: `{1}`ms |         |
| (Log-5) | UsingDeviceStateChanged	     | Using device state change detected! DeviceName=`{0}` DeviceId=`{1}` |         |

## Miscellaneous words ##
|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
| (Word-1) | EventDriven	                 | Event driven | WASAPI **event driven** data feed mode |
| (Word-2) | TimerDriven	                 | Timer driven | WASAPI **timer driven** data feed mode |
| (Word-3) | Exclusive	                   | Exclusive    | WASAPI **exclusive** operation mode |
| (Word-4) | Shared	                      | Shared       | WASAPI **shared** operation mode |
| (Word-5) | Failed	                      | Failed       |         |
| ~~(Word-6)~~ | FloatingPointNumbers	        | Float        | not need to translate |
| (Word-7) | Latency	                     | Latency      | means output latency (delay) time |
| (Word-8) | Error	                       | Error        |         |
| (Word-9) | ErrorCode	                   | Error code   |         |
| (Word-10)| ValidBits	                   | Valid bits   | There is 32bit per sample PCM data with 24bit valid bits and 8bit padding zeroes |
| (Word-11)| Version	                     | Version      |         |

## Open File filters ##
|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
| (Filter-1) | FilterCueFiles	              | CUE files    |         |
| (Filter-2) | FilterPpwplFiles	            | PPWPL files  |         |
| (Filter-3) | FilterSupportedFiles	        | Supported files |         |
| (Filter-4) | FilterM3uFiles               | M3U files    |         |

## Setup failed dialog message ##
```
Please confirm sample rate or quantization bit rate are supported on your device using "list supported format" feature. Press [Clear the playlist][List supported format].
  * Some professional audio devices (RME Fireface, M-Audio ProFire, Echo AudioFire etc.) are not able to be changed master sampling rate via WASAPI. Please use FireFace settings, M-Audio ProFire control panel or equivalent tools on your system tray to change master sampling rate of those devices before playback.
  * 44.1kHz and 88.2kHz sample rate of Creative X-Fi Titanium HD do work using timer driven mode.
  * Creative USB Sound Blaster HD prefers event driven mode. Timer driven mode tends to more problematic on some devices.
  * There are strange audio devices which stops TOSLINK output when no signal. Such devices apparently seem not to deliver first several thousand samples of PCM data. If first part of music is truncated, please increase [Zero flush period on playback starts] value.
  * PlayPcmWin cannot setup E-MU 0404 USB 24bit mode. [Detailed settings][Fix Int16] to play 24bit files on those devices but it truncates lower 8bit information.
  * Halide Bridge seems to require 24bit data always. [Detailed settings][Fix Int24] to play sound on those devices.
  * Lynx AES16e prefers timer-driven mode and small latency value such as 29 ms.
  * Some HDMI audio devices prefer 28 ms to 31 ms latency value.
  * Halide Bridge and DSPeaker Anti-Mode 2.0 Dual core seem to require 24bit data always.     [Detailed settings][Fix Int24] to play sound on those devices.
```

## Settings window ##

![http://bitspersampleconv2.googlecode.com/files/ppwLocalizeSettings.png](http://bitspersampleconv2.googlecode.com/files/ppwLocalizeSettings.png)

|No.| Resource identifier          | English text | comment |
|:--|:-----------------------------|:-------------|:--------|
| (S-1) | SettingsWindowTitle          |PlayPcmWin detailed settings|         |
| (S-2) | SettingsLabelQuantizationBitrate|Quantization bit rate to pass through the WASAPI(Converts on file loading)|(max approx. 120 characters) |
| (S-3) | (removed)                    |(removed)     |(max approx. 120 characters) |
| (S-4) | (removed)                    |(removed)     |(max approx. 120 characters) |
| (S-5) | (removed)                    |(removed)     |(max approx. 120 characters) |
| (S-6) | SettingsRadioButtonBpsSint16 |16bit integer(Truncates lower bits when 24bps or 32bps data arrives)|(max approx. 120 characters) |
| (S-7) | SettingsRadioButtonBpsSint24 |24bit integer(Fills 0 to lower bits when 16bps data arrives, Truncates lower bits when 32bps data arrives)| (max approx. 120 characters) |
| (S-8) | SettingsRadioButtonBpsSint32V24|32bit integer, valid bits=24(Int32, Valid bits=24bps)|(max approx. 120 characters) |
| (S-9) | SettingsRadioButtonBpsSint32 |32bit integer(Fill 0s to lower bits, Valid bits=32bps)| (max approx. 120 characters)|
| (S-10) | SettingsRadioButtonBpsSfloat32|Binary32(IEEE754 single precision floating point format: Supported hardware is very rare)| (max approx. 120 characters)|
| (S-11) | SettingsRadioButtonBpsAutoSelect|Auto select   |(max approx. 120 characters) |
| (S-12) | SettingsGroupBoxCuesheetSettings|CUE sheet settings|         |
| (S-13) | SettingsCheckBoxPlaceKokomadeAterIndex00|Set [read endpoint](Batch.md) flag after INDEX00|         |
| (S-14) | SettingsGroupBoxDeviceBufferFlush|Device buffer flush settings|         |
| (S-15) | SettingsLabelZeroFlushSeconds|Zero flush period on playback starts:|         |
| (S-16) | SettingsLabelZeroFlushUnit   |second(s)     |         |
| (S-17) | SettingsGroupBoxRenderThreadTaskType|Render thread task type|         |
|~~(S-18)~~| SettingsRadioButtonTaskAudio |Audio         | (no need to translate) |
| ~~(S-19)~~ | SettingsRadioButtonTaskProAudio|Pro Audio     | (no need to translate) |
| ~~(S-20)~~ | SettingsRadioButtonTaskNone  |Playback      | (no need to translate) |
| ~~(S-21)~~ | SettingsRadioButtonTaskPlayback|None          | (no need to translate) |
| (S-22) | SettingsGroupBoxDisplaySettings|List display settings|         |
| (S-23) | SettingsCheckBoxAlternateBackground|Alternating row background|         |
| (S-24) | SettingsButtonChangeColor    |Change color...|         |
| (S-25) | (removed)                    |WASAPI Shared Resampler| (max approx. 30 characters) |
| (S-26) | SettingsLabelConversionQuality|Resampler MFT Quality (1 to 60):|         |
| (S-27) | (removed)                    |              | |
| (S-28) | SettingsCheckBoxManuallySetMainWindowDimension|Remember Window position and size| (max approx. 30 characters) |
| (S-29) | SettingsCheckBoxStorePlaylistContent|Restore the playlist on program startup|(max approx. 30 characters) |
| (S-30) | SettingsCheckBoxCoverart     |Display cover art images|(max approx. 30 characters) |
| (S-31) | SettingsCheckBoxRefrainRedraw|Minimize GUI redraw when playing| (max approx. 30 characters)|
| (S-32) | SettingsCheckBoxParallelRead |Parallelize file read|(max approx. 30 characters) |
| (S-33) | SettingsCheckBoxTimePeriod1  |Invoke timeBeginPeriod(1)| **timeBeginPeriod(1)** is invoked function name.(max approx. 30 characters) |
| (S-34) | SettingsLabelPlayingTimeFont |Playing time font:|         |
| (S-35) | SettingsCheckBoxPlayingTimeBold|Bold          | means font style is bold |
| (S-36) | SettingsButtonReset          |`_Restore defaults`|         |
| (S-37) | SettingsButtonOK             |OK            |         |
| (S-38) | SettingsButtonCancel         |Cancel        |         |
| - | SettingsLabelFontPoints      |pt            | means font size unit is pt (points). no need to translate |
| (S-39) | SettingsGroupBoxWasapiShared |WASAPI shared mode settings| group box title |
| (S-40) | SettingsCueEncoding          |Character encoding:| cue sheet character encoding |
| (S-41) | SettingsTimerResolution1Millisec|Timer resolution = 1 ms| ms means milliseconds |
| (S-42) | SettingsTimerResolution500Microsec|Timer reso = 0.5 ms (unstable)| (max approx. 30 characters) |
| (S-43) | SettingsTimerResolutionDefault|Default timer resolution| (max approx. 30 characters) |
| (S-44) | SettingsSootheLimiterApo     |Scale maximum magnitude to 0.98 to soothe limiter APO| Limiter APO is technical term. magnitude means absolute sample value |
| (S-45) | SettingsGroupBoxWasapiExclusive|WASAPI exclusive mode settings| Groupbox title text |
| (S-46) | SettingsCheckboxSortDropFolder|Sort dropped folder items| (max approx. 30 characters) |
| (S-47) | SettingsCheckBoxSetBatchReadEndpoint|Set [read endpoint](Batch.md) on file drop| (max approx. 30 characters) |
| (S-48) | SettingsCheckBoxSortDroppedFiles|Sort drag&dropped files| (max approx. 30 characters) |
| (S-49) | SettingsGroupBoxFile         |File read settings| Groupbox title text |
| (S-50) | SettingsGroupBoxPlaybackThread|Playback thread settings| Groupbox title text |
| (S-51) | SettingsCheckBoxVerifyFlacMD5Sum|Verify MD5 sum on FLAC file read| (max approx. 30 characters) |
| (S-52) | SettingsCheckBoxGpuRendering |Use GPU for UI draw| GPU is technical term. UI means user interface. (max approx. 30 characters) |
| (S-53) | SettingsGroupBoxTimerResolution|Timer resolution|Groupbox title text |
| (S-54) | SettingsLabelNoiseShaping    |Noise shaping option:|         |
| (S-55) | SettingsNoNoiseShaping       |Just trancate lower bits when bits per sample is reduced|         |
| (S-56) | SettingsPerformDitheredNoiseShaping|Perform dithered noise shaping when bits per sample is reduced|         |
| (S-57) | SettingsPerformNoiseShaping  |Perform noise shaping when bits per sample is reduced|         |
| (S-58) | SettingsPerformDither        |Add dither when bits per sample is reduced|         |