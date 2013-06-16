#pragma once

// 日本語 UTF-8
// WWPlayPcmGroup 同一フォーマットのPCMデータのリンクリストを保持するクラス。

#include <Windows.h>
#include <vector>
#include "WWPcmData.h"
#include <assert.h>

// PCMデータのセット方法
//     1. Clear()を呼ぶ。
//     2. AddPlayPcmDataStart()を呼ぶ。
//     3. PCMデータの数だけAddPlayPcmData()を呼ぶ。
//     4. AddPlayPcmDataEnd()を呼ぶ。
//     5. SetPlayRepeat()を呼ぶ。
// 注1: サンプルフォーマット変換は上のレイヤーに任せた。
//      ここでは、来たdataを中のメモリにそのままコピーする。
//      Setupでセットアップした形式でdataを渡してください。
// 注2: AddPlayPcmDataEnd()後に、
//      Clear()をしないでAddPlayPcmData()することはできません。
class WWPlayPcmGroup {
public:
    WWPlayPcmGroup(void) {
        m_repeat = false;
        Clear();
    }

    ~WWPlayPcmGroup(void) {
        assert(m_playPcmDataList.size() == 0);
    }

    void Clear(void) {
        for (size_t i=0; i<m_playPcmDataList.size(); ++i) {
            m_playPcmDataList[i].Term();
        }
        m_playPcmDataList.clear();

        m_sampleFormat  = WWPcmDataSampleFormatUnknown;
        m_sampleRate    = 0;
        m_numChannels   = 0;
        m_dwChannelMask = 0;
        m_bytesPerFrame = 0;
    }

    void Init(void);
    void Term(void);

    /// @param sampleFormat データフォーマット。
    /// @param bytesPerFrame 1フレームのバイト数。
    ///     ＝(1サンプル1チャンネルのバイト数×チャンネル数)
    bool AddPlayPcmDataStart(
            int sampleRate,
            WWPcmDataSampleFormatType sampleFormat,
            int numChannels,
            DWORD dwChannelMask,
            int bytesPerFrame);

    /// @param id WAVファイルID。
    /// @param data WAVファイルのPCMデータ。LRLRLR…で、リトルエンディアン。
    ///             data==NULLの場合、PCMデータのメモリ領域だけ確保。
    /// @param bytes dataのバイト数。
    /// @return true: 追加成功。false: 追加失敗。
    bool AddPlayPcmData(int id, BYTE *data, int64_t bytes);

    void AddPlayPcmDataEnd(void);
    
    void RemoveAt(int id);

    void SetPlayRepeat(bool b);

    WWPcmData *FindPcmDataById(int id);

    WWPcmData *FirstPcmData(void);
    WWPcmData *LastPcmData(void);

    WWPcmData *NthPcmData(int n);
    int Count(void) const { return (int)m_playPcmDataList.size(); }

    bool GetRepatFlag(void) { return m_repeat; }

    /// @return S_OK: success
    HRESULT DoResample(int sampleRate, WWPcmDataSampleFormatType sampleFormat, int numChannels, DWORD dwChannelMask, int conversionQuality);

private:
    std::vector<WWPcmData> m_playPcmDataList;

    WWPcmDataSampleFormatType m_sampleFormat;
    int                 m_sampleRate;
    int                 m_numChannels;
    DWORD               m_dwChannelMask;
    int                 m_bytesPerFrame;

    bool                m_repeat;

    void PlayPcmDataListDebug(void);
};
