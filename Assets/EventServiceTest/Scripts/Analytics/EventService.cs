using Assets.EventServiceTest.Scripts.Files;
using Assets.EventServiceTest.Scripts.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.EventServiceTest.Scripts
{
    public class EventService : MonoBehaviour
    {
        [SerializeField]
        private string serverUrl = string.Empty;

        [SerializeField]
        [Tooltip("Interval between sends in seconds")]
        private uint cooldownBeforeSend = 15;

        [SerializeField]
        private NetworkRequester networkRequester = default;

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

            Assert.IsTrue(serverUrl != string.Empty);
            Assert.IsTrue(cooldownBeforeSend > 0);
        }

        private void Start()
        {
            StartCoroutine(SendEventsRoutine());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

            SaveEvents();
        }

        private void InitializeEventsContainer()
        {
            eventsData = new List<EventData>();

            eventsToSend = new EventsToSend();

            LoadEvents();
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
            networkRequester.SendPostRequest(serverUrl, dataToSend, SendEventsPostRequestCompletedCallback);
        }

        private void SendEventsPostRequestCompletedCallback(bool isSuccess)
        {
            eventsToSend.events.Clear();
        }

        private void SaveEvents()
        {
            eventsToSend.events.AddRange(eventsData);

            string serializedEvents = JsonUtility.ToJson(eventsToSend);
            FileSerializer.Save(FileSerializer.ANALYTICS_EVENTS_FILE_NAME, serializedEvents);
        }

        private void LoadEvents()
        {
            string serializedEvents = FileSerializer.Load(FileSerializer.ANALYTICS_EVENTS_FILE_NAME);

            eventsToSend = JsonUtility.FromJson<EventsToSend>(serializedEvents);
        }
    }
}
