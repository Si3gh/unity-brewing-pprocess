using System;
using UnityEngine;
using Object = System.Object;

namespace Request.Comment
{
    public class UpvoteComment : MonoBehaviour
    {
        private HttpRequest _httpRequest;
        private CommentIntegration _commentIntegration;

        private readonly Guid _trackId = Guid.Parse("49f712d1-a596-4691-a609-114d5e9d23d8");
        private readonly Guid _stageId = Guid.Parse("759b1b42-f0cc-4b17-bb57-698dcbaded10");
        private Guid _consultantId;
        private long _dialogIndex;

            void Awake()
        {
            _httpRequest = FindObjectOfType<HttpRequest>();
            _commentIntegration = FindObjectOfType<CommentIntegration>();
        }

        public void Upvote()
        {
            var currentComment = _commentIntegration.GetCurrentComment();
            StartCoroutine(_httpRequest.PostRequest($"comment/{_trackId}/{_stageId}/{currentComment.stageIndex}/{currentComment.consultantId}", null));
        }
    }
}
