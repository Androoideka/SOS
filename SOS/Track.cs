using System.Collections.Generic;

namespace SOS
{
    class Track
    {
        public List<MIDIEvent> e = new List<MIDIEvent>();
        public Soundbank instrument;
        public Track(Soundbank inst)
        {
            instrument = inst;
        }
    }
}
