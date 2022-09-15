using UnityEngine;

public class EventService : MonoBehaviour
{    
    public void TrackEvent(string type, string data)
    {
        Debug.Log($"New event received: type = {type}, data = {data}");
    }
}
