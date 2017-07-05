using System;
using NAudio.Wave;

namespace SOS
{
    public class PrototypeWaveOffsetStream32 : WaveStream
    {
        private WaveStream sourceStream;
        private long audioStartPosition;
        private long sourceLengthBytes;
        private long length;
        private readonly int bytesPerSample; // includes all channels
        private long position;
        private TimeSpan startTime;
        private TimeSpan sourceLength;
        private readonly object lockObject = new object();
        public PrototypeWaveOffsetStream32(WaveStream sourceStream, TimeSpan startTime)
        {
            if (sourceStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                throw new ArgumentException("Only IEEE float supported");
            this.sourceStream = sourceStream;
            position = 0;
            bytesPerSample = sourceStream.WaveFormat.Channels * sourceStream.WaveFormat.BitsPerSample / 8;
            this.StartTime = startTime;
            this.SourceLength = sourceStream.TotalTime;
        }
        public TimeSpan StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                lock (lockObject)
                {
                    startTime = value;
                    audioStartPosition = (long)(startTime.TotalSeconds * sourceStream.WaveFormat.SampleRate) * bytesPerSample;
                    // fix up our length and position
                    this.length = audioStartPosition + sourceLengthBytes;
                    Position = Position;
                }
            }
        }
        public TimeSpan SourceLength
        {
            get
            {
                return sourceLength;
            }
            set
            {
                lock (lockObject)
                {
                    sourceLength = value;
                    sourceLengthBytes = (long)(sourceLength.TotalSeconds * sourceStream.WaveFormat.SampleRate) * bytesPerSample;
                    // fix up our length and position
                    this.length = audioStartPosition + sourceLengthBytes;
                    Position = Position;
                }
            }
        }
        public override int BlockAlign
        {
            get
            {
                return sourceStream.BlockAlign;
            }
        }
        public override long Length
        {
            get
            {
                return length;
            }
        }
        public override long Position
        {
            get
            {
                return position;
            }
            set
            {
                lock (lockObject)
                {
                    // make sure we don't get out of sync
                    value -= (value % BlockAlign);
                    if (value < audioStartPosition)
                        sourceStream.Position = 0;
                    else
                        sourceStream.Position = (value - audioStartPosition);
                    position = value;
                }
            }
        }
        public override int Read(byte[] destBuffer, int offset, int numBytes)
        {
            lock (lockObject)
            {
                int bytesWritten = 0;
                // fill with silence
                if (position < audioStartPosition)
                {
                    bytesWritten = (int)Math.Min(numBytes, audioStartPosition - position);
                    for (int n = 0; n < bytesWritten; n++)
                        destBuffer[n + offset] = 0;
                }
                if (bytesWritten < numBytes)
                {
                    int sourceBytesRequired = (int)Math.Min(
                        numBytes - bytesWritten,
                        sourceLengthBytes - sourceStream.Position);
                    int read = sourceStream.Read(destBuffer, bytesWritten + offset, sourceBytesRequired);
                    bytesWritten += read;
                }
                return numBytes;
            }
        }
        public override WaveFormat WaveFormat
        {
            get
            {
                return sourceStream.WaveFormat;
            }
        }
        public override bool HasData(int count)
        {
            if (position + count < audioStartPosition)
                return false;
            if (position >= length)
                return false;
            // Check whether the source stream has data.
            // source stream should be in the right poisition
            return sourceStream.HasData(count);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sourceStream != null)
                {
                    sourceStream.Dispose();
                    sourceStream = null;
                }
            }
            else
            {
                System.Diagnostics.Debug.Assert(false, "WaveOffsetStream was not Disposed");
            }
            base.Dispose(disposing);
        }
    }
}