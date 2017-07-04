using System;

namespace SOS
{
    class Note
    {
        struct Message
        {
            public int elapse;
            public int velocityChange;
        }
        public sbyte pitch, velocity;
        public Note(int i)
        {
            pitch = Convert.ToSByte(i);
            velocity = 0;
        }
    }
}
