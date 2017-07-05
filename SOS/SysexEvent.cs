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
            if (file[i + length] != 247 && eventType == 240)
            {
                data = new byte[length + 1];
                data[length] = 247;
            }
            else
                data = new byte[length];
            for (int j = 0; j < length; j++)
            {
                data[j] = file[i];
                i++;
            }
        }
        internal override int CalculateChunks(Event runningStatus)
        {
            int i = base.CalculateChunks(runningStatus) + 1;
            i += CalculateVLV(data.Length).Length / 7;
            i += data.Length;
            return i;
        }
        internal override byte[] WriteEvent(Event runningStatus, ref int n)
        {
            n = 0;
            byte[] e = base.WriteEvent(runningStatus, ref n);
            byte[] temp = WriteVLV(data.Length);
            for (int i = 0; i < temp.Length; i++)
                e[n + i] = temp[i];
            n += temp.Length;
            for (int i = 0; i < data.Length; i++)
                e[n + i] = data[i];
            n += data.Length;
            return e;
        }
    }
}
