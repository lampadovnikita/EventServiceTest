using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.EventServiceTest.Scripts.Network
{
    public class NetworkRequester : MonoBehaviour
    {
        public void SendPostRequest(string uri, string contentToSend, Action<bool> completedCallback)
        {
            StartCoroutine(SendPostRequestRoutine(uri, contentToSend, completedCallback));
        }

        private IEnumerator SendPostRequestRoutine(string uri, string contentToSend, Action<bool> completedCallback)
        {
            bool isRequestSuccessful = true;

            using var webRequest = UnityWebRequest.Post(uri, UnityWebRequest.kHttpVerbPOST);
            webRequest.timeout = 5;

            if (string.IsNullOrEmpty(contentToSend) == false)
            {
                Debug.Log($"[{uri}] Send post request with: {contentToSend}");

                var bytes = System.Text.Encoding.UTF8.GetBytes(contentToSend);
                webRequest.uploadHandler = new UploadHandlerRaw(bytes) { contentType = "application/json" };
            }
            else
            {
                Debug.Log($"[{uri}] Send Post request without content to send");
            }

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"[{uri}] Success");
            }
            else
            {
                isRequestSuccessful = false;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                        Debug.LogError($"[{uri}] Connection Error: {webRequest.error}");
                        break;
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError($"[{uri}] Data Processing Error: {webRequest.error}");
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError($"[{uri}] Protocol Error: {webRequest.error}");
                        break;
                }
            }

            completedCallback?.Invoke(isRequestSuccessful);
        }
    }
}