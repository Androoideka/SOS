namespace SOS
{
    class MIDIEvent : Event
    {
        public readonly byte velocity;
        public readonly byte note;
        public readonly byte channel;
        public MIDIEvent(int dt, byte ch, byte vel, byte no) : base(dt, 9)
        {
            channel = ch;
            velocity = vel;
            note = no;
        }
        public MIDIEvent(int dt, byte ch, byte vel) : base(dt, 12)
        {
            channel = ch;
            velocity = vel;
        }
        private static byte DetermineEventType(byte ec, Event runningStatus)
        {
            //Note On 9
            //Polyphonic Key Pressure 10
            //Control Change 11
            //Program Change 12
            //Aftertouch 13
            //Pitchbend 14
            int chv = ec - 144, i = 9;
            while (chv > 15 && i < 15)
            {
                chv -= 16;
                i++;
            }
            if (i > 14)
                return (runningStatus as MIDIEvent).eventType;
            return (byte)i;
        }
        public MIDIEvent(int dt, byte[] file, Event runningStatus, ref int i) : base(dt, DetermineEventType(file[i], runningStatus))
        {
            if (runningStatus.eventType > 8 && runningStatus.eventType < 15 && file[i] < 128)
                channel = (runningStatus as MIDIEvent).channel;
            else
            {
                int chv = file[i] - 144;
                while (chv > 15)
                    chv -= 16;
                channel = (byte)chv;
                i++;
            }
            if (eventType == 12 || eventType == 13)
            {
                velocity = file[i];
                i++;
            }
            else
            {
                note = file[i];
                i++;
                velocity = file[i];
                i++;
            }
        }
        internal override int CalculateChunks(Event runningStatus)
        {
            int i = base.CalculateChunks(runningStatus);
            if ((runningStatus as MIDIEvent) != null && Status() != (runningStatus as MIDIEvent).Status())
                i++;
            i++;
            if (eventType != 12 && eventType != 13)
                i++;
            return i;
        }
        internal byte Status()
        {
            int status = eventType * 16;
            status += channel;
            return (byte)status;
        }
        internal override byte[] WriteEvent(Event runningStatus, ref int n)
        {
            n = 0;
            byte[] e = base.WriteEvent(runningStatus, ref n);
            if ((runningStatus as MIDIEvent) != null && Status() != (runningStatus as MIDIEvent).Status())
            {
                e[n] = Status();
                n++;
            }
            if(eventType != 12 && eventType != 13)
            {
                e[n] = note;
                n++;
            }
            e[n] = velocity;
            n++;
            return e;
        }
    }
}