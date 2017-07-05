namespace SOS
{
    class SysexEvent : Event
    {
        public readonly byte[] data;
        public SysexEvent(int dt, byte et, byte[] d) : base(dt, et)
        {
            data = d;
        }
        public SysexEvent(int dt, byte[] file, ref int i) : base(dt, file[i])
        {
            i++;
            int length = base.ReadVLV(file, ref i);
            data = new byte[length];
            for (int j = 0; j < length; j++)
            {
                data[j] = file[i];
                i++;
            }
        }
        internal int CalculateChunks(byte runningStatus)
        {
            int i = 1;
            i += CalculateVLV(data.Length).Length / 7;
            i += data.Length;
            return i;
        }
    }
}
