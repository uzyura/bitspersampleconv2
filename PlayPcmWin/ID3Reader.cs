﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayPcmWin {
    class ID3Reader {
        public enum ID3Result {
            Success,
            ReadError,
            NotSupportedID3version,
        }

        public string AlbumName { get; set; }
        public string TitleName { get; set; }
        public string ArtistName { get; set; }

        /// <summary>
        /// true: unsynchroする(ファイルから0xffが出てきたら次のバイトを捨てる)
        /// </summary>
        private bool m_unsynchro;

        /// <summary>
        /// Unsynchroの捨てデータも含めた、読み込み可能バイト数
        /// </summary>
        private long m_bytesRemain;
        private long m_readBytes;

        public long ReadBytes { get { return m_readBytes; } }

        private void Clear() {
            AlbumName = string.Empty;
            TitleName = string.Empty;
            ArtistName = string.Empty;
            m_unsynchro = false;
            m_bytesRemain = 0;
            m_readBytes = 0;
        }

        private byte[] BinaryReadBytes(BinaryReader br, int bytes) {
            byte[] result = br.ReadBytes(bytes);
            m_bytesRemain -= bytes;
            m_readBytes   += bytes;
            return result;
        }

        private byte BinaryReadByte(BinaryReader br) {
            byte result = br.ReadByte();
            --m_bytesRemain;
            ++m_readBytes;
            return result;
        }

        /// <summary>
        /// unsynchronizationを考慮したバイト列読み出し処理
        /// </summary>
        private byte[] ReadBytesWithUnsynchro(BinaryReader br, int bytes) {
            if (m_unsynchro) {
                // unsynchroする
                var buff = new System.Collections.Generic.List<byte>();
                for (int i=0; i < bytes; ++i) {
                    var b = BinaryReadByte(br);
                    if (b == 0xff) {
                        BinaryReadByte(br);
                    }
                    buff.Add(b);
                }
                return buff.ToArray();
            } else {
                // unsynchroしない
                var result = BinaryReadBytes(br, bytes);
                return result;
            }
        }

        /// <summary>
        /// unsynchronizationを考慮したバイト列スキップ。
        /// </summary>
        /// <param name="bytes">unsynchro前のバイト数。unsynchro時は、スキップするバイト列に0xffが現れるとこれよりも多くスキップする</param>
        private void SkipBytesWithUnsynchro(BinaryReader br, long bytes) {
            if (m_unsynchro) {
                // unsynchroする
                for (long i=0; i < bytes; ++i) {
                    var b = BinaryReadByte(br);
                    --m_bytesRemain;
                    ++m_readBytes;
                    if (b == 0xff) {
                        BinaryReadByte(br);
                        --m_bytesRemain;
                        ++m_readBytes;
                    }
                }
                return;
            } else {
                // unsynchroしない
                PcmDataLib.Util.BinaryReaderSkip(br, bytes);
                m_bytesRemain -= bytes;
                m_readBytes   += bytes;
            }
        }

        private UInt16 ByteArrayToBigU16(byte[] bytes, int offset=0) {
            return (UInt16)(((UInt16)bytes[offset+0] << 8) +
                            ((UInt16)bytes[offset+1] << 0));
        }

        private UInt32 ByteArrayToBigU24(byte[] bytes, int offset = 0) {
            return  (UInt32)((UInt32)bytes[offset + 0] << 16) +
                    (UInt32)((UInt32)bytes[offset + 1] << 8) +
                    (UInt32)((UInt32)bytes[offset + 2] << 0);
        }

        private UInt32 ByteArrayToBigU32(byte[] bytes, int offset = 0) {
            return  (UInt32)((UInt32)bytes[offset+0] << 24) +
                    (UInt32)((UInt32)bytes[offset+1] << 16) +
                    (UInt32)((UInt32)bytes[offset+2] << 8) +
                    (UInt32)((UInt32)bytes[offset+3] << 0);
        }

        private int ID3TagHeaderSize(byte[] sizeBytes) {
            System.Diagnostics.Debug.Assert(sizeBytes.Length == 4);

            return  ((int)(sizeBytes[0] & 0x7f) << 21) +
                    ((int)(sizeBytes[1] & 0x7f) << 14) +
                    ((int)(sizeBytes[2] & 0x7f) << 7) +
                    ((int)(sizeBytes[3] & 0x7f) << 0);
        }

        private byte[] m_tagVersion;

        private ID3Result ReadTagHeader(BinaryReader br) {
            var tagIdentifier = BinaryReadBytes(br, 3);
            if (tagIdentifier[0] != 'I' ||
                    tagIdentifier[1] != 'D' ||
                    tagIdentifier[2] != '3') {
                return ID3Result.ReadError;
            }
            m_tagVersion = BinaryReadBytes(br, 2);
            if (m_tagVersion[0] != 2 && m_tagVersion[0] != 3) {
                // not ID3v2.2 nor ID3v2.3...
                return ID3Result.NotSupportedID3version;
            }
            var tagFlags = BinaryReadByte(br);
            m_bytesRemain = ID3TagHeaderSize(BinaryReadBytes(br, 4));

            if (0 != (tagFlags & 0x80)) {
                // Unsynchronizationモード(0xffを読んだら次の1バイトを捨てる)
                m_unsynchro = true;
            } else {
                m_unsynchro = false;
            }

            // ID3v2 tagヘッダー読み込み終了。

            // ID3v2.3 extended headerがもしあれば読み込む。
            if (0 != (tagFlags & 0x40)) {
                var ehSize  = ByteArrayToBigU32(ReadBytesWithUnsynchro(br, 4));
                var ehFlags = ByteArrayToBigU16(ReadBytesWithUnsynchro(br, 2));
                var ehPad   = ByteArrayToBigU32(ReadBytesWithUnsynchro(br, 4));
                SkipBytesWithUnsynchro(br, ehSize + ehPad - 6);
            }
            return ID3Result.Success;
        }

        private string ReadNameFrame(BinaryReader br, int frameSize, int frameFlags) {
            // フラグを見る
            bool compression = (frameFlags & 0x0080) != 0;
            bool encryption  = (frameFlags & 0x0040) != 0;

            if (compression || encryption) {
                SkipFrameContent(br, frameSize, frameFlags);
                return string.Empty;
            }

            var encoding = ReadBytesWithUnsynchro(br, 1);
            var text = ReadBytesWithUnsynchro(br, frameSize - 1);

            string result = string.Empty;
            switch (encoding[0]) {
            case 0: // ISO-8859-1だがSJISで読む
                result = System.Text.Encoding.Default.GetString(text, 0, text.Length).Trim(new char[] { '\0' });
                break;
            case 1: // UTF-16 with BOM
                if (text[0] == 0xfe && text[1] == 0xff) {
                    // UTF-16BE
                    result = System.Text.Encoding.BigEndianUnicode.GetString(text, 2, text.Length - 2).Trim(new char[] { '\0' });
                } else if (text[0] == 0xff && text[1] == 0xfe) {
                    // UTF-16LE
                    result = System.Text.Encoding.Unicode.GetString(text, 2, text.Length - 2).Trim(new char[] { '\0' });
                } else {
                    // unknown encoding!
                }
                break;
            case 2: // UTF-16BE without BOM
                result = System.Text.Encoding.BigEndianUnicode.GetString(text, 2, text.Length - 2).Trim(new char[] { '\0' });
                break;
            case 3: // UTF-8
                result = System.Text.Encoding.UTF8.GetString(text, 2, text.Length - 2).Trim(new char[] { '\0' });
                break;
            default:
                // Unknown encoding!
                break;
            }
            return result;
        }

        private void SkipFrameContent(BinaryReader br, int frameSize, int frameFlags) {
            ReadBytesWithUnsynchro(br, frameSize);
        }

        private ID3Result ReadFramesV23(BinaryReader br) {
            // ID3v2.3 frameを読み込み。
            while (0 < m_bytesRemain) {
                // ID3v2 frame header
                var frameId    = ByteArrayToBigU32(ReadBytesWithUnsynchro(br, 4));
                int frameSize  = (int)ByteArrayToBigU32(ReadBytesWithUnsynchro(br, 4));
                var frameFlags = ByteArrayToBigU16(ReadBytesWithUnsynchro(br, 2));

                switch (frameId) {
                case 0x54414c42:
                    // "TALB" アルバム名
                    AlbumName = ReadNameFrame(br, frameSize, frameFlags);
                    break;
                case 0x54495432:
                    // "TIT2" 曲名
                    TitleName = ReadNameFrame(br, frameSize, frameFlags);
                    break;
                case 0x54504531:
                    // "TPE1" メインアーティスト
                    ArtistName = ReadNameFrame(br, frameSize, frameFlags);
                    break;
                case 0:
                    // 終わり
                    return ID3Result.Success;
                default:
                    SkipFrameContent(br, frameSize, frameFlags);
                    break;
                }
            }

            return ID3Result.Success;
        }

        private ID3Result ReadFramesV22(BinaryReader br) {
            // ID3v2.2 frameを読み込み。
            while (0 < m_bytesRemain) {
                // ID3v2 frame header
                var frameId    = ByteArrayToBigU24(ReadBytesWithUnsynchro(br, 3));
                int frameSize  = (int)ByteArrayToBigU24(ReadBytesWithUnsynchro(br, 3));

                Console.WriteLine("ReadFramesV22 {5:X8} \"{0}{1}{2}\" {3}bytes remain={4}",
                    (char)(0xff & (frameId >> 16)),
                    (char)(0xff & (frameId >> 8)),
                    (char)(0xff & (frameId >> 0)),
                    frameSize,
                    m_bytesRemain,
                    frameId);

                switch (frameId) {
                case 0x54414c:
                    // "TAL" アルバム名
                    AlbumName = ReadNameFrame(br, frameSize, 0);
                    break;
                case 0x545432:
                    // "TT2" 曲名
                    TitleName = ReadNameFrame(br, frameSize, 0);
                    break;
                case 0x545031:
                    // "TP1" メインアーティスト
                    ArtistName = ReadNameFrame(br, frameSize, 0);
                    break;
                case 0:
                    // 終わり
                    return ID3Result.Success;
                default:
                    SkipFrameContent(br, frameSize, 0);
                    break;
                }
            }

            return ID3Result.Success;
        }

        public ID3Result Read(BinaryReader br) {
            Clear();

            // ID3v2 tagヘッダー読み込み。
            var result = ReadTagHeader(br);
            if (result != ID3Result.Success) {
                return result;
            }

            switch (m_tagVersion[0]) {
            case 3:
                return ReadFramesV23(br);
            case 2:
                return ReadFramesV22(br);
            default:
                System.Diagnostics.Debug.Assert(false);
                return ID3Result.ReadError;
            }
        }

    }
}
