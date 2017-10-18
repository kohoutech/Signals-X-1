using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Signals.IO
{
    public class AudioFile
    {
        String filename;
        public uint sampleRate;
        public uint numChannels;
        public uint dataSize;
        int datapos;

        public AudioFile(String name)
        {
            filename = name;
        }

        Byte[] srcbuf;
        uint srclen;
        uint srcpos;

        public void loadWavFile()
        {
            srcbuf = File.ReadAllBytes(filename);
            srclen = (uint)srcbuf.Length;
            srcpos = 0;

            //wave hdr
            String sig = getSig();
            uint hdrsize = getFour();
            String format = getSig();
            Console.WriteLine("wav file sig = {0}, size = {1}, format = {2}", sig, hdrsize, format);

            //fmt chunk
            String subchunkID = getSig();
            uint subchunkSize = getFour();
            uint audioFormat = getTwo();
            numChannels = getTwo();
            sampleRate = getFour();
            uint byteRate = getFour();
            uint blockAlign = getTwo();
            uint bitsPerSample = getTwo();
            Console.WriteLine("chunk sig = {0}", subchunkID);
            Console.WriteLine("chunk size = {0}, audio format = {1}, num channels = {2}", subchunkSize, audioFormat, numChannels);
            Console.WriteLine("sample rate = {0}, byte rate = {1}, blockAlign = {2}, bits/sample = {3}", sampleRate, byteRate, blockAlign, bitsPerSample);

            //data chunk
            String dataID = getSig();
            dataSize = getFour();
            datapos = (int)srcpos;
            Console.WriteLine("data sig = {0}, data size = {1}m data ofs = {2}", dataID, dataSize, datapos);
        }

        public float[] getWavData() {

            float[] data = new float[dataSize];
            for (int i = 0, j = datapos; i < dataSize; i++, j++)
            {
                data[i] = srcbuf[j] / 255.0f;
            }
            return data;
        }

        //-----------------------------------------------------------------------------

        public String getSig()
        {
            char a = (char)srcbuf[srcpos++];
            char b = (char)srcbuf[srcpos++];
            char c = (char)srcbuf[srcpos++];
            char d = (char)srcbuf[srcpos++];
            String result = "" + a + b + c + d;
            return result;
        }

        public uint getTwo()
        {
            byte b = srcbuf[srcpos++];
            byte a = srcbuf[srcpos++];
            uint result = (uint)(a * 256 + b);
            return result;
        }

        public uint getFour()
        {
            byte d = srcbuf[srcpos++];
            byte c = srcbuf[srcpos++];
            byte b = srcbuf[srcpos++];
            byte a = srcbuf[srcpos++];
            uint result = (uint)(a * 256 + b);
            result = (result * 256 + c);
            result = (result * 256 + d);
            return result;
        }
    }
}
