using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Request.Comment
{
    public class CommentIntegration : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI commentTextField;
        [SerializeField] private List<GameObject> historyElements = new List<GameObject>();
        [SerializeField] private List<ButtonSelect> fillHistoryElements = new List<ButtonSelect>();
#pragma warning restore 0649

        private HttpRequest _httpRequest;
        private UpvoteComment _upvoteCommentIntegration;

        private readonly List<Comment> _comments = new List<Comment>();

        private List<Comment> CurrentIndexComments => _comments
            .Where(comment => comment.stageIndex == _stageIndex)
            .ToList();

        private int MaxCommentSize => GetMaxNumberOfComments(_stageIndex);

        private int _currentIndex;

        private int _stageIndex;

        private bool _wasUsed;

        void Awake()
        {
            _upvoteCommentIntegration = FindObjectOfType<UpvoteComment>();
            _httpRequest = FindObjectOfType<HttpRequest>();
        }

        public void GetComments(Guid stage)
        {
            StartCoroutine(_httpRequest.GetRequest<Comment[]>($"comment/{stage}", InitObject));
        }

        public int GetMaxNumberOfComments(int stageIndex)
        {
            return _comments.Count(comment => comment.stageIndex == stageIndex);
        }

        private void InitObject(Comment[] comments)
        {
            _wasUsed = false;
            _comments.AddRange(comments);
            UpdateUi();
            EnableHistory();
            FillButton();
        }

        private void EnableHistory()
        {
            var enabledHistorySize = MaxCommentSize > historyElements.Count ? historyElements.Count : MaxCommentSize;
            foreach (var historyElement in historyElements)
            {
                historyElement.SetActive(false);
            }

            for (var count = 0; count < enabledHistorySize; count++)
            {
                historyElements[count].SetActive(true);
            }
        }

        public void ShowCommentsInStageDialog(int stageIndex)
        {
            if (stageIndex == _stageIndex && _wasUsed)
            {
                EnableHistory();
                UpdateUi();
                return;
            }

            if (_comments.Any(comment => comment.stageIndex == stageIndex))
            {
                _stageIndex = stageIndex;
                _wasUsed = true;
                _currentIndex = 0;

                EnableHistory();
                UpdateUi();
            }
            else
            {
                Debug.LogError($"Nenhum comentario nesse stage index cara. StageIndex: {stageIndex}");
            }
        }

        public void ShowNext()
        {
            if (CurrentIndexComments.Count > _currentIndex + 1)
            {
                _currentIndex++;
                UpdateUi();
            }
        }

        public void ShowPrevious()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                UpdateUi();
            }
        }

        public Comment GetCurrentComment()
        {
            return CurrentIndexComments[_currentIndex];
        }

        private void UpdateUi()
        {
            if (CurrentIndexComments.Count > _currentIndex)
            {
                commentTextField.text = CurrentIndexComments[_currentIndex].comment;
                FillButton();
                _upvoteCommentIntegration.UpdateButtons();
            }
        }

        private void FillButton()
        {
            foreach (var buttonFill in fillHistoryElements)
            {
                buttonFill.Disable();
            }

            fillHistoryElements[_currentIndex].Enable();
        }

        public void ShowByIndex(int index)
        {
            if (_comments.Count > index && index >= 0)
            {
                _currentIndex = index;
                UpdateUi();
            }
        }
    }

    [Serializable]
    public class Comment
    {
        public string consultantId;

        public string comment;

        public int stageIndex;

        public long upvoteCount;

        public bool upvotedByUser;
    }
}