namespace SOS
{
    class MetaEvent : Event
    {
        public readonly byte type;
        public readonly byte[] data;
        public MetaEvent(int dt, byte t, byte[] d) : base(dt, 255)
        {
            type = t;
            data = d;
        }
        public MetaEvent(int dt, byte[] file, ref int i) : base(dt, 255)
        {
            i++;
            type = file[i];
            i++;
            int length = ReadVLV(file, ref i);
            data = new byte[length];
            for (int j = 0; j < length; j++)
            {
                data[j] = file[i];
                i++;
            }
        }
        //SHOULD BE USING AN OVERRIDE!
        internal override int CalculateChunks(Event runningStatus)
        {
            int i = base.CalculateChunks(runningStatus) + 2;
            i += CalculateVLV(data.Length).Length / 7;
            i += data.Length;
            return i;
        }
        internal override byte[] WriteEvent(Event runningStatus, ref int n)
        {
            n = 0;
            byte[] e = base.WriteEvent(runningStatus, ref n);
            e[n] = type;
            n++;
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
