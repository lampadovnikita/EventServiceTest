using Assets.EventServiceTest.Scripts.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.EventServiceTest.Scripts
{
    public class EventCatcher : MonoBehaviour
    {
        [SerializeField]
        private MockEventsUI mockEventsUI = default;

        [SerializeField]
        private EventService eventService = default;

        private void Awake()
        {
            Assert.IsNotNull(mockEventsUI);
            Assert.IsNotNull(eventService);
        }

        private void Start()
        {
            mockEventsUI.OnEventTracked += OnEventTracked;
        }

        private void OnDestroy()
        {
            mockEventsUI.OnEventTracked -= OnEventTracked;
        }

        private void OnEventTracked(string type, string data)
        {
            eventService.TrackEvent(type, data);
        }
    }
}