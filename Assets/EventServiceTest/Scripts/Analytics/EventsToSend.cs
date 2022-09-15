using System;
using System.Collections.Generic;

namespace Assets.EventServiceTest.Scripts
{
    [Serializable]
    public class EventsToSend
    {
        public List<EventData> events;

        public EventsToSend()
        {
            events = new List<EventData>();
        }
    }
}