# How to use Resampler MFT #

MFT(Media Foundation transform) is a component of Microsoft Media Foundation. MFT provides media data processing.

Resampler MFT (MFT interface of Audio Resampler DSP) is a sample rate converter introduced on Windows 7.

Resampler MFT is implemented as a Media Foundation Transform.
Its input/output data is uncompressed little-endian byte order PCM audio stream.

# Sample code #

Working sample program is avaiable here
http://code.google.com/p/bitspersampleconv2/source/browse/#svn%2Ftrunk%2FWWMFResamplerTest

Compiled executable
http://code.google.com/p/bitspersampleconv2/downloads/detail?name=WWMFResamplerTest107.zip

## 1. Initialization ##

Media Foundation uses COM. Call CoInitializeEx to initialize COM.

Next call MFStartup to initialize Media Foundation platform.

```
HRESULT hr = S_OK;

hr = CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);
// check if SUCCEEDED(hr)

hr = MFStartup(MF_VERSION, MFSTARTUP_NOSOCKET);
// check if SUCCEEDED(hr)
```

## 2. Create Resampler MFT Object ##

```
CComPtr<IUnknown> spTransformUnk;
IMFTransform *pTransform = NULL; //< this is Resampler MFT

hr = CoCreateInstance(CLSID_CResamplerMediaObject, NULL, CLSCTX_INPROC_SERVER,
        IID_IUnknown, (void**)&spTransformUnk);

hr = spTransformUnk->QueryInterface(IID_PPV_ARGS(&pTransform));

CComPtr<IWMResamplerProps> spResamplerProps;
hr = spTransformUnk->QueryInterface(IID_PPV_ARGS(&spResamplerProps);
hr = spResamplerProps->SetHalfFilterLength(60); //< best conversion quality
```

## 3. Specify input/output PCM format to Resampler MFT ##

```
IMFMediaType *pMediaType = NULL;
MyPcmFormat fmt;
// set input PCM format parameters to fmt

MFCreateMediaType(&pMediaType);
hr = pMediaType->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio));
hr = pMediaType->SetGUID(MF_MT_SUBTYPE,
        (fmt.sampleFormat == MyBitFormatInt) ? MFAudioFormat_PCM : MFAudioFormat_Float);
hr = pMediaType->SetUINT32(MF_MT_AUDIO_NUM_CHANNELS,         fmt.nChannels);
hr = pMediaType->SetUINT32(MF_MT_AUDIO_SAMPLES_PER_SECOND,   fmt.sampleRate);
hr = pMediaType->SetUINT32(MF_MT_AUDIO_BLOCK_ALIGNMENT,      fmt.FrameBytes());
hr = pMediaType->SetUINT32(MF_MT_AUDIO_AVG_BYTES_PER_SECOND, fmt.BytesPerSec());
hr = pMediaType->SetUINT32(MF_MT_AUDIO_BITS_PER_SAMPLE,      fmt.bits);
hr = pMediaType->SetUINT32(MF_MT_ALL_SAMPLES_INDEPENDENT,    TRUE);
if (0 != fmt.dwChannelMask) {
    hr = pMediaType->SetUINT32(MF_MT_AUDIO_CHANNEL_MASK, fmt.dwChannelMask);
}
if (fmt.bits != fmt.validBitsPerSample) {
    hr = pMediaType->SetUINT32(MF_MT_AUDIO_VALID_BITS_PER_SAMPLE, fmt.validBitsPerSample);
}
pTransform->SetInputType(0, spOutputType, 0);

// also need to call pTransform->SetOutputType() in the same manner as SetInputType
```

## 4. Send stream start message to Resampler MFT ##

```
hr = pTransform->ProcessMessage(MFT_MESSAGE_COMMAND_FLUSH, NULL);
hr = pTransform->ProcessMessage(MFT_MESSAGE_NOTIFY_BEGIN_STREAMING, NULL);
hr = pTransform->ProcessMessage(MFT_MESSAGE_NOTIFY_START_OF_STREAM, NULL);
```

## 5. Create IMFSample from uncompressed PCM input data ##
```
BYTE  *data = ...; //< input PCM data 
DWORD bytes = ...; //< bytes need to be smaller than approx. 1Mbytes

IMFMediaBuffer *pBuffer = NULL;
hr = MFCreateMemoryBuffer(bytes , &pBuffer);

BYTE  *pByteBufferTo = NULL;
hr = pBuffer->Lock(&pByteBufferTo, NULL, NULL);
memcpy(pByteBufferTo, data, bytes);
pBuffer->Unlock();
pByteBufferTo = NULL;

hr = pBuffer->SetCurrentLength(bytes);

IMFSample *pSample = NULL;
hr = MFCreateSample(&pSample);
hr = pSample->AddBuffer(pBuffer);

SafeRelease(&pBuffer);
```

## 6. Set input data to Resampler MFT ##

```
hr = pTransform->ProcessInput(0, pSample, 0);
```

## 7. Perform sample rate conversion and get output sample data ##

```
do {
    MFT_OUTPUT_DATA_BUFFER outputDataBuffer;
    DWORD dwStatus;
    hr = pTransform->ProcessOutput(0, 1, &outputDataBuffer, &dwStatus);
    if (hr == MF_E_TRANSFORM_NEED_MORE_INPUT) {
        // conversion end
        break;
    }

    // output PCM data is set in outputDataBuffer.pSample;
    IMFSample *pSample = outputDataBuffer.pSample;
 
    CComPtr<IMFMediaBuffer> spBuffer;
    hr = pSample->ConvertToContiguousBuffer(&spBuffer);
    DWORD cbBytes = 0;
    hr = spBuffer->GetCurrentLength(&cbBytes);

    BYTE  *pByteBuffer = NULL;
    hr = spBuffer->Lock(&pByteBuffer, NULL, NULL);

    BYTE *to = new BYTE[cbBytes]; //< output PCM data
    toBytes = cbBytes;            //< output PCM data size
    memcpy(to, pByteBuffer, cbBytes);

    spBuffer->Unlock();
} while (true);
```

## 8. Repeat 5, 6 and 7 until all input data is processed ##

## 9. Drain remaining samples on Resampler MFT buffer ##

```
hr = pTransform->ProcessMessage(MFT_MESSAGE_NOTIFY_END_OF_STREAM, NULL);
hr = pTransform->ProcessMessage(MFT_MESSAGE_COMMAND_DRAIN, NULL);

// perform step 7
```

## 10. Send end streaming message to Resampler MFT and shutdown Media Framework ##

```
pTransform->ProcessMessage(MFT_MESSAGE_NOTIFY_END_STREAMING, NULL);
SafeRelease(&pTransform);
MFShutdown();
CoUninitialize();
```

# References #

Audio Resampler DSP
http://msdn.microsoft.com/en-us/library/windows/desktop/ff819070%28v=vs.85%29.aspx

Basic MFT Processing Model
http://msdn.microsoft.com/en-us/library/windows/desktop/aa965264%28v=vs.85%29.aspx

## WWMFResamplerTest Changelog ##

  * version 1.0.7 : fixed : 24-bit WAV file read bug ([Issue 137](https://code.google.com/p/bitspersampleconv2/issues/detail?id=137))
  * version 1.0.6 : fixed : WAV file data chunk size calculation bug
  * version 1.0.4 : fixed : WAV file chunk skipping size calculation bug
  * version 1.0.3 : fixed : 44.1kHz to 192kHz conversion
  * version 1.0.2 : fixed : PCM sample is not counted properly.
> > print file open error.
  * version 1.0.1 : fixed : PCM is not drained properly
  * version 1.0.0 : initial release