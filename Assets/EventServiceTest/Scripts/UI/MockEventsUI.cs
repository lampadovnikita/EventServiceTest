using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.EventServiceTest.Scripts.UI
{
    public class MockEventsUI : MonoBehaviour
    {
        // first argument - event's type
        // second - event's data
        public event Action<string, string> OnEventTracked;

        private List<MockEventRowUI> mockEventRows;

        private void Awake()
        {
            mockEventRows = new List<MockEventRowUI>(GetComponentsInChildren<MockEventRowUI>());
        }

        private void Start()
        {
            foreach (MockEventRowUI eventRowUI in mockEventRows)
            {
                eventRowUI.OnEventRowTrackEvent += OnEventRowTrackedEvent;
            }
        }

        private void OnEventRowTrackedEvent(string type, string data)
        {
            OnEventTracked?.Invoke(type, data);
        }
    }
}
