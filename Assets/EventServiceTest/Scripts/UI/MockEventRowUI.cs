using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class MockEventRowUI : MonoBehaviour
{
    // first argument - event's type
    // second - event's data
    public event Action<string, string> OnEventRowTrackEvent;

    [SerializeField]
    private string type = default;

    [SerializeField]
    private Button trackEventButton = default;

    [SerializeField]
    private TMP_InputField valueInputField = default;

    private void Awake()
    {
        Assert.IsNotNull(trackEventButton);
        Assert.IsNotNull(valueInputField);

        Assert.IsTrue(type != string.Empty);
    }

    private void Start()
    {
        trackEventButton.onClick.AddListener(OnTrackEventButtonClicked);
    }

    private void OnDestroy()
    {
        trackEventButton.onClick.RemoveListener(OnTrackEventButtonClicked);
    }

    private void OnTrackEventButtonClicked()
    {
        string enteredValue = valueInputField.text;

        if (enteredValue == string.Empty)
        {
            return;
        }

        OnEventRowTrackEvent?.Invoke(type, enteredValue);
    }
}
