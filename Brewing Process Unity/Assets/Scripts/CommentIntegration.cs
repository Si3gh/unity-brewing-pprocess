using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CommentIntegration : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI commentTextField;

    [SerializeField] private List<GameObject> historyElements = new List<GameObject>();

    private HttpRequest _httpRequest;

    private List<CommentDto> _comments = new List<CommentDto>();

    private int _currentIndex = 0;

    private List<CommentDto> _currentIndexComments => _comments.Where(comment => comment.stageIndex == _stageIndex)
        .ToList();

    private int _maxCommentSize => _comments.Count(comment => comment.stageIndex == _stageIndex);

    private int _stageIndex;

    void Awake()
    {
        _httpRequest = FindObjectOfType<HttpRequest>();
    }

    public void GetComments(Guid stage)
    {
        StartCoroutine(_httpRequest.GetRequest<CommentDto[]>($"comment/{stage}", InitObject));
    }

    private void InitObject(CommentDto[] comments)
    {
        _comments.AddRange(comments);
        SetText();
        EnableHistory();
    }

    private void EnableHistory()
    {
        var enabledHistorySize = _maxCommentSize > historyElements.Count ? historyElements.Count : _maxCommentSize;
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
        if (_comments.Any(comment => comment.stageIndex == stageIndex))
        {
            _stageIndex = stageIndex;
            EnableHistory();
        }
        else
        {
            Debug.LogError($"Nenhum comentario nesse stage index cara. StageIndex: {stageIndex}");
        }
    }

    public void ShowNext()
    {
        if (_currentIndexComments.Count > _currentIndex + 1)
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

    private void SetText()
    {
        if (_currentIndexComments.Count > _currentIndex)
        {
            commentTextField.text = _currentIndexComments[_currentIndex].comment;
        }
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
    public string comment;

    public int stageIndex;
}