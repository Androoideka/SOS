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
        internal virtual int CalculateChunks(Event runningStatus)
        {
            int i = CalculateVLV(deltaTime).Length / 7;
            return i;
        }
        internal virtual byte[] WriteEvent(Event runningStatus, ref int n)
        {
            n = 0;
            byte[] e = new byte[CalculateChunks(runningStatus)];
            byte[] temp = WriteVLV(deltaTime);
            for (int i = 0; i < temp.Length; i++)
                e[i] = temp[i];
            n = temp.Length;
            e[n] = eventType;
            n++;
            return e;
        }
    }
}