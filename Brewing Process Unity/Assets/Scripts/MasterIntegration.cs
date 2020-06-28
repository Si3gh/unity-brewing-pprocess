using System.Collections.Generic;
using UnityEngine;

public class MasterIntegration : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _dialogues = null;

    [SerializeField]
    private List<ButtonSelect> _historiesButton = null;

    [SerializeField]
    private GameObject playButtonSet = null;

    [SerializeField]
    private GameObject continueButtonSet = null;

    [SerializeField]
    private GameObject continueWithCommentButtonSet = null;

    public int CurrentDialogue { get; private set; }

    private CommentIntegration _commentIntegration;

    public void Start()
    {
        _commentIntegration = FindObjectOfType<CommentIntegration>();
        ShowCorrectButtonSet();
    }

    public void ShowNextDialogue()
    {
        if (CurrentDialogue + 1 < _dialogues.Count)
        {
            CurrentDialogue++;
        }

        ShowCorrectButtonSet();
    }

    public void ShowDialogueByIndex(int index)
    {
        if (index < _dialogues.Count)
        {
            CurrentDialogue = index;
        }

        ShowCorrectButtonSet();
    }

    public void ShowCorrectButtonSet()
    {
        ShowDialogueText();
        if (CurrentDialogue == _dialogues.Count - 1)
        {
            ShowPlayButtonSet();
            return;
        }

        int commentCount = _commentIntegration.GetMaxNumberOfComments(CurrentDialogue);

        if (commentCount > 0)
        {
            ShowContinueWithCommentButtonSet();
        }
        else
        {
            ShowContinueButtonSet();
        }
    }

    private void ShowDialogueText()
    {
        foreach (var dialogue in _dialogues)
        {
            dialogue.SetActive(false);
        }

        foreach (var button in _historiesButton)
        {
            button.Disable();
        }

        _historiesButton[CurrentDialogue].Enable();
        _dialogues[CurrentDialogue].SetActive(true);
    }

    private void ShowPlayButtonSet()
    {
        playButtonSet.SetActive(true);
        continueButtonSet.SetActive(false);
        continueWithCommentButtonSet.SetActive(false);
    }

    private void ShowContinueWithCommentButtonSet()
    {
        continueWithCommentButtonSet.SetActive(true);
        continueButtonSet.SetActive(false);
        playButtonSet.SetActive(false);
    }

    private void ShowContinueButtonSet()
    {
        continueButtonSet.SetActive(true);
        continueWithCommentButtonSet.SetActive(false);
        playButtonSet.SetActive(false);
    }
}
