// 日本語 UTF-8
#include "WWAudioFilterSequencer.h"
#include "WWAudioFilter.h"
#include <assert.h>

WWAudioFilterSequencer::WWAudioFilterSequencer(void)
      : m_format(WWPcmDataSampleFormatSint16),
        m_streamType(WWStreamPcm),
        m_numChannels(2),
        m_audioFilter(nullptr)
{
}

WWAudioFilterSequencer::~WWAudioFilterSequencer(void)
{
    assert(m_audioFilter == nullptr);
}

void
WWAudioFilterSequencer::Init(void)
{
    UnregisterAll();
}

void
WWAudioFilterSequencer::Term(void)
{
    UnregisterAll();
}

WWAudioFilter *
WWAudioFilterSequencer::Last(void)
{
    if (m_audioFilter == nullptr) {
        return nullptr;
    }

    WWAudioFilter *p = m_audioFilter;

    while (p->Next() != nullptr) {
        p = p->Next();
    }
    return p;
};

void
WWAudioFilterSequencer::Append(WWAudioFilter *af)
{
    af->UpdateSampleFormat(m_format, m_streamType, m_numChannels);
    af->SetNext(nullptr);

    if (m_audioFilter == nullptr) {
        m_audioFilter = af;
    } else {
        WWAudioFilter *p = Last();
        p->SetNext(af);
    }
}

void
WWAudioFilterSequencer::Loop(std::function<void(WWAudioFilter*)> f)
{
    WWAudioFilter *p = m_audioFilter;
    while (p != nullptr) {
        WWAudioFilter *next = p->Next();

        f(p);

        p = next;
    }
}

void
WWAudioFilterSequencer::UnregisterAll(void)
{
    Loop([](WWAudioFilter *p) {
        delete p;
    });

    m_audioFilter = nullptr;
}

void
WWAudioFilterSequencer::UpdateSampleFormat(WWPcmDataSampleFormatType format, WWStreamType streamType, int numChannels)
{
    m_format = format;
    m_streamType = streamType;
    m_numChannels = numChannels;

    Loop([format, streamType, numChannels](WWAudioFilter*p) {
        p->UpdateSampleFormat(format, streamType, numChannels);
    });
}

void
WWAudioFilterSequencer::ProcessSamples(unsigned char *buff, int bytes)
{
    Loop([buff,bytes](WWAudioFilter*p) {
        p->Filter(buff, bytes);
    });
}