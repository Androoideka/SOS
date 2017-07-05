using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace SOS
{
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        public readonly MixingSampleProvider mixer;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            outputDevice.Play();
        }
        public void Dispose()
        {
            outputDevice.Dispose();
        }
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}