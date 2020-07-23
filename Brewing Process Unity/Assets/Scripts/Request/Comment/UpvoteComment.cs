using System;
using UnityEngine;
using Object = System.Object;

namespace Request.Comment
{
    public class UpvoteComment : MonoBehaviour
    {
        private HttpRequest _httpRequest;

        void Awake()
        {
            _httpRequest = FindObjectOfType<HttpRequest>();            
        }

        public void Upvote(Guid trackId, Guid stageId, Guid consultantId, long dialogIndex)
        {
            StartCoroutine(_httpRequest.PostRequest($"{trackId}/{stageId}/{dialogIndex}/{consultantId}", null));
        }
    }
}
