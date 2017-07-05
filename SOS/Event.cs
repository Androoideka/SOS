using System;

namespace SOS
{
    public class Event
    {
        public readonly int deltaTime;
        public readonly byte eventType;
        public Event(int dt, byte et)
        {
            deltaTime = dt;
            eventType = et;
        }
        protected int ReadVLV(byte[] file, ref int i)
        {
            string totalRes = "";
            bool continueReading = true;
            while (continueReading)
            {
                string vlv = Convert.ToString(file[i], 2);
                while (vlv.Length != 8)
                    vlv = 0 + vlv;
                if (file[i] < 128)
                    continueReading = false;
                totalRes += vlv.Substring(1);
                i++;
            }
            return Convert.ToInt32(totalRes, 2);
        }
        protected byte[] WriteVLV(int val)
        {
            string vlv = CalculateVLV(val);
            byte[] chunks = new byte[vlv.Length / 7];
            for (int i = 0; i < chunks.Length; i++)
            {
                string num = "";
                if (i != chunks.Length - 1)
                    num += "1";
                else
                    num += "0";
                num += vlv.Substring(i * 6, 7);
                chunks[i] = Convert.ToByte(num, 2);
            }
            return chunks;
        }
        protected string CalculateVLV(int val)
        {
            string vlv = Convert.ToString(val, 2);
            while (vlv.Length % 7 != 0)
                vlv = 0 + vlv;
            return vlv;
        }
        internal int CalculateChunks()
        {
            int i = CalculateVLV(deltaTime).Length / 7;
            if (eventType == 255)
                i += (this as MetaEvent).CalculateChunks();
            else if (eventType > 200)
                i += (this as SysexEvent).CalculateChunks();
            else
                i += (this as MIDIEvent).CalculateChunks();
            return i;
        }
        internal virtual byte[] WriteEvent()
        {
            byte[] e = new byte[CalculateChunks()];
            int n = 0;
            byte[] temp = WriteVLV(deltaTime);
            for (int i = 0; i < temp.Length; i++)
                e[i] = temp[i];
            n = temp.Length;
            return e;
        }
    }
}