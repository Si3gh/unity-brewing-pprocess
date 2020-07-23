using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Request.Comment
{
    public class CommentIntegration : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI commentTextField = null;

        [SerializeField] 
        private List<GameObject> historyElements = new List<GameObject>();

        [SerializeField]
        private List<ButtonSelect> fillHistoryElements = new List<ButtonSelect>();

        private HttpRequest _httpRequest;

        private readonly List<CommentDto> _comments = new List<CommentDto>();

        private List<CommentDto> CurrentIndexComments => _comments
            .Where(comment => comment.stageIndex == _stageIndex)
            .ToList();

        private int MaxCommentSize => GetMaxNumberOfComments(_stageIndex);
    
        private int _currentIndex;

        private int _stageIndex;

        private bool _wasUsed;

        void Awake()
        {
            _httpRequest = FindObjectOfType<HttpRequest>();
        }

        public void GetComments(Guid stage)
        {
            StartCoroutine(_httpRequest.GetRequest<CommentDto[]>($"comment/{stage}", InitObject));
        }

        public int GetMaxNumberOfComments(int stageIndex)
        {
            return _comments.Count(comment => comment.stageIndex == stageIndex);
        }

        private void InitObject(CommentDto[] comments)
        {
            _wasUsed = false;
            this._comments.AddRange(comments);
            SetText();
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
                SetText();
                return;
            }

            if (_comments.Any(comment => comment.stageIndex == stageIndex))
            {
                _stageIndex = stageIndex;
                _wasUsed = true;

                EnableHistory();
                SetText();
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
            }

            SetText();
        }

        public void ShowPrevious()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
            }

            SetText();
        }

        public CommentDto GetCurrentComment()
        {
            return CurrentIndexComments[_currentIndex];
        }

        private void SetText()
        {
            if (CurrentIndexComments.Count > _currentIndex)
            {
                commentTextField.text = CurrentIndexComments[_currentIndex].comment;
                FillButton();
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
            }

            SetText();
        }
    }

    [Serializable]
    public class CommentDto
    {
        public string consultantId;
        
        public string comment;

        public int stageIndex;
    }
}