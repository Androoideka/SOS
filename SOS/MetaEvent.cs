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
        protected int CalculateChunks(byte runningStatus)
        {
            int i = 2;
            i += CalculateVLV(data.Length).Length / 7;
            i += data.Length;
            return i;
        }
        internal override void WriteEvent()
        {

        }
    }
}
