using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PlayerData;
using UnityEngine;
using UnityEngine.Networking;

namespace Request
{
    public class HttpRequest : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private EnviromentPicker enviroment;
        [SerializeField] private ErrorWindow errorWindow;
#pragma warning restore 0649

        private RequestUrl _requestUrl;

        public void Awake()
        {
            _requestUrl = enviroment.GetUrl();
        }

        public IEnumerator GetRequest<T>(string sufixo, Action<T> callbackSuccess,
            Dictionary<string, string> requestHeaders = null)
        {
            ValidateUrl();

            var request = UnityWebRequest.Get($"{_requestUrl.url}/{sufixo}");

            SetRequestHeaders(requestHeaders, request);

            yield return request.SendWebRequest();

            HandleRequest(callbackSuccess, request);
        }


        public IEnumerator PostRequest<T>(string sufixo, object body, Action<T> callbackSuccess,
            Dictionary<string, string> requestHeaders = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbPOST;

            yield return request.SendWebRequest();

            HandleRequest(callbackSuccess, request);
        }

        public IEnumerator PostRequest(string sufixo, object body = null,
            Dictionary<string, string> requestHeaders = null, Action callback = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbPOST;

            yield return request.SendWebRequest();

            HandleNoContentResponse(request, callback);
        }

        public IEnumerator PutRequest<T>(string sufixo, object body, Action<T> callbackSuccess,
            Dictionary<string, string> requestHeaders = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbPUT;

            yield return request.SendWebRequest();

            HandleRequest(callbackSuccess, request);
        }

        public IEnumerator PutRequest(string sufixo, object body = null,
            Dictionary<string, string> requestHeaders = null, Action callback = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbPUT;

            yield return request.SendWebRequest();

            HandleNoContentResponse(request, callback);
        }

        public IEnumerator DeleteRequest<T>(string sufixo, object body, Action<T> callbackSuccess,
            Dictionary<string, string> requestHeaders = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbDELETE;

            yield return request.SendWebRequest();

            HandleRequest(callbackSuccess, request);
        }

        public IEnumerator DeleteRequest(string sufixo, object body = null,
            Dictionary<string, string> requestHeaders = null, Action callback = null)
        {
            UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
            request.method = UnityWebRequest.kHttpVerbDELETE;

            yield return request.SendWebRequest();

            HandleNoContentResponse(request, callback);
        }

        private void SetRequestHeaders(Dictionary<string, string> requestHeaders, UnityWebRequest request)
        {
            if (requestHeaders != null)
            {
                foreach (var requestHeader in requestHeaders)
                {
                    request.SetRequestHeader(requestHeader.Key, requestHeader.Value);
                }
            }

            request.SetRequestHeader("content-type", "application/json");
            request.SetRequestHeader("Guest-Id", PlayerPreferences.GetPlayerId().ToString());
        }

        private UnityWebRequest BuildWebRequest(string sufixo, object body, Dictionary<string, string> requestHeaders)
        {
            ValidateUrl();

            var bodyAsJson = JsonUtility.ToJson(body);

            var request = new UnityWebRequest($"{_requestUrl.url}/{sufixo}")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            if (body != null)
            {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyAsJson));
            }

            SetRequestHeaders(requestHeaders, request);

            return request;
        }


        private void HandleRequest<T>(Action<T> callbackSuccess, UnityWebRequest request)
        {
            if (request.isNetworkError || request.isHttpError)
            {
                HandleError(request);
            }
            else
            {
                var responseBody = JsonUtility.FromJson<BaseResponse<T>>(request.downloadHandler.text);
                Debug.Log("Request Success. \n" +
                          $"Status Code: {request.responseCode} \n" +
                          $"Response: {responseBody}");
                callbackSuccess(responseBody.data);
            }
        }

        private void HandleNoContentResponse(UnityWebRequest request, Action callback)
        {
            if (request.isNetworkError || request.isHttpError)
            {
                HandleError(request);
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }
        private void ValidateUrl()
        {
            if (!_requestUrl.isValid)
            {
                Debug.Log("Não está apontando para produção");
            }
        }

        private void HandleError(UnityWebRequest request)
        {
            var errorBody = JsonUtility.FromJson<ErrorObject>(request.downloadHandler.text);
            Debug.LogError("Request error. \n" +
                           $"Status Code: {request.responseCode}\n" +
                           $"Response: {errorBody}");
            HandleError(errorBody?.data.message);
        }

        private void HandleError(string errorMessage)
        {
            errorWindow.ShowError(errorMessage);
        }

        [Serializable]
        public class BaseResponse<TR>
        {
            public DateTime executed;

            public TR data;
        }

        [Serializable]
        public class ErrorObject
        {
            public DateTime executed;

            public Message data;

            [Serializable]
            public class Message
            {
                public string message;
            }
        }
    }
}