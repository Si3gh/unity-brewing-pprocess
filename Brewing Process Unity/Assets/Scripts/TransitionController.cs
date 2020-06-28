using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private GameObject _consultorDialogue = null;

    [SerializeField]
    private GameObject _masterDialogue = null;

    private CommentIntegration _commentIntegration;
    
    private MasterIntegration _masterIntegration;
    
    void Start()
    {
        _commentIntegration = gameObject.GetComponent<CommentIntegration>();
        _masterIntegration = gameObject.GetComponent<MasterIntegration>();
    }

    public void ShowMasterDialogue()
    {
        _consultorDialogue.SetActive(false);
        _masterDialogue.SetActive(true);
    }

    public void ShowConsultorDialogue()
    {
        var stageIndex = _masterIntegration.CurrentDialogue;
        _masterDialogue.SetActive(false);
        _consultorDialogue.SetActive(true);
        _commentIntegration.ShowCommentsInStageDialog(stageIndex);
    }
}
