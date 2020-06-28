using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    private ErrorWindow errorWindow;

    [SerializeField]
    private string APIUrl;

    void Awake()
    {
        errorWindow = gameObject.GetComponentInChildren<ErrorWindow>();   
    }

    public IEnumerator GetRequest<T>(string sufixo, Action<T> callbackSuccess, Dictionary<string, string> requestHeaders = null)
    {
        var request = UnityWebRequest.Get($"{APIUrl}/{sufixo}");

        if (requestHeaders != null)
        {
            SetRequestHeaders(requestHeaders, request);
        }

        yield return request.SendWebRequest();

        HandleRequest(callbackSuccess, request);
    }

    public IEnumerator PostRequest<T>(string sufixo, object body, Action<T> callbackSuccess, Dictionary<string, string> requestHeaders = null)
    {
        UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
        request.method = UnityWebRequest.kHttpVerbPOST;

        yield return request.SendWebRequest();

        HandleRequest(callbackSuccess, request);
    }

    public IEnumerator PutRequest<T>(string sufixo, object body, Action<T> callbackSuccess, Dictionary<string, string> requestHeaders = null)
    {
        UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
        request.method = UnityWebRequest.kHttpVerbPUT;

        yield return request.SendWebRequest();

        HandleRequest(callbackSuccess, request);
    }

    public IEnumerator DeleteRequest<T>(string sufixo, object body, Action<T> callbackSuccess, Dictionary<string, string> requestHeaders = null)
    {
        UnityWebRequest request = BuildWebRequest(sufixo, body, requestHeaders);
        request.method = UnityWebRequest.kHttpVerbDELETE;

        yield return request.SendWebRequest();

        HandleRequest(callbackSuccess, request);
    }

    private void SetRequestHeaders(Dictionary<string, string> requestHeaders, UnityWebRequest request)
    {
        foreach (var requestHeader in requestHeaders)
        {
            request.SetRequestHeader(requestHeader.Key, requestHeader.Value);
        }
        request.SetRequestHeader("content-type", "application/json");
    }

    private UnityWebRequest BuildWebRequest(string sufixo, object body, Dictionary<string, string> requestHeaders)
    {
        var bodyAsJson = JsonUtility.ToJson(body);
        var request = new UnityWebRequest($"{APIUrl}/{sufixo}");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyAsJson));
        request.downloadHandler = new DownloadHandlerBuffer();
        if (requestHeaders != null)
        {
            SetRequestHeaders(requestHeaders, request);
        }
        return request;
    }

    private void HandleRequest<T>(Action<T> callbackSuccess, UnityWebRequest request)
    {
        if (request.isNetworkError || request.isHttpError)
        {
            var errorBody = JsonUtility.FromJson<ErrorObject>(request.downloadHandler.text);
            Debug.LogError("Request error. \n" +
                $"Status Code: {request.responseCode}\n" +
                $"Response: {errorBody}");
            HandleError(errorBody.data.message);
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

    private void HandleError(string errorMessage)
    {
        errorWindow.ShowError(errorMessage);
    }

    [Serializable]
    public class BaseResponse<R>
    {
        public DateTime executed;

        public R data;
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

