using System;
using UnityEngine;

namespace Request.Comment
{
    public class UpvoteComment : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private GameObject likedButton;
        [SerializeField] private GameObject dislikedButton;
#pragma warning restore 0649

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
            StartCoroutine(
                _httpRequest.PostRequest(
                    $"comment/{_trackId}/{_stageId}/{currentComment.stageIndex}/{currentComment.consultantId}",
                    callback: () => ToggleUpvote(currentComment)
                )
            );
        }

        public void Downvote()
        {
            var currentComment = _commentIntegration.GetCurrentComment();
            StartCoroutine(_httpRequest.DeleteRequest(
                    $"comment/{_trackId}/{_stageId}/{currentComment.stageIndex}/{currentComment.consultantId}",
                    callback: () => ToggleUpvote(currentComment)
                ));
        }

        public void UpdateButtons()
        {
            var currentComment = _commentIntegration.GetCurrentComment();
            DisableLikeButton(currentComment.upvotedByUser);
        }

        private void ToggleUpvote(Comment comment)
        {
            comment.upvotedByUser = !comment.upvotedByUser;
            DisableLikeButton(comment.upvotedByUser);
        }

        private void DisableLikeButton(bool isUpvoted)
        {
            likedButton.SetActive(isUpvoted);
            dislikedButton.SetActive(!isUpvoted);
        }
    }
}