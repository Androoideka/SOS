using System;
using System.Timers;
using NAudio.Wave;

namespace SOS
{
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingWaveProvider32 mixer;
        private Timer tmr;
        private bool lok = true;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            mixer = new MixingWaveProvider32();
            mixer.AddInputStream(new BufferedWaveProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2)));
            if (AsioOut.isSupported())
                outputDevice = new AsioOut(0);
            else
            {
                outputDevice = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 300);
                System.Windows.Forms.MessageBox.Show("No ASIO drivers detected. Falling back on WASAPI");
            }
            outputDevice.Init(mixer);
            outputDevice.Play();
        }
        public void Dispose()
        {
            outputDevice.Dispose();
        }
        public void Play(IWaveProvider pst, double length)
        {
            if (lok)
            {
                tmr = new Timer(length + 100);
                tmr.AutoReset = false;
                tmr.Elapsed += Elapse;
                tmr.Start();
                mixer.AddInputStream(pst);
                lok = false;
            }
        }
        private void Elapse(object sender, ElapsedEventArgs e)
        {
            lok = true;
        }
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}