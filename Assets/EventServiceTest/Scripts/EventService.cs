using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.EventServiceTest.Scripts
{
    public class EventService : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Interval between sends in seconds")]
        private uint cooldownBeforeSend = 15;

        private List<EventData> eventsData;

        private EventsToSend eventsToSend;

        public void TrackEvent(string type, string data)
        {
            if (eventsData == null)
            {
                InitializeEventsContainer();
            }

            eventsData.Add(new EventData(type, data));
        }

        private void Awake()
        {
            if (eventsData == null)
            {
                InitializeEventsContainer();
            }
        }

        private void Start()
        {
            StartCoroutine(SendEventsRoutine());
        }

        private void InitializeEventsContainer()
        {
            eventsData = new List<EventData>();

            eventsToSend = new EventsToSend();
        }

        private IEnumerator SendEventsRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldownBeforeSend);

                SendEvents();
            }
        }

        private void SendEvents()
        {
            eventsToSend.events.AddRange(eventsData);
            eventsData.Clear();

            string dataToSend = JsonUtility.ToJson(eventsToSend);
            Debug.Log($"Send data: {dataToSend}");

            eventsToSend.events.Clear();
        }
    }
}
