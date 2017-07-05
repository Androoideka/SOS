using System;
using NAudio.Wave;

namespace SOS
{
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            if(AsioOut.isSupported())
                outputDevice = new AsioOut(0);
            //else
                //outputDevice = new WasapiOut()
        }
        public void Dispose()
        {
            outputDevice.Dispose();
        }
        public void Play(IWaveProvider pst)
        {
            outputDevice.Init(pst);
            outputDevice.Play();
        }
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}