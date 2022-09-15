using System;
using UnityEngine;

namespace Assets.EventServiceTest.Scripts.Analytics
{
    [Serializable]
    public class EventData
    {
        [SerializeField]
        private string type;

        [SerializeField]
        private string data;

        public string Type => type;

        public string Data => data;

        public EventData(string type, string data)
        {
            this.type = type;

            this.data = data;
        }
    }
}