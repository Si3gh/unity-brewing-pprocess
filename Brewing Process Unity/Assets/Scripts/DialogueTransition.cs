using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTransition : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    [SerializeField]
    private GameObject dialogueHistory;

    public void ActivateDialogueButton()
    {
        dialogueHistory.SetActive(true);
    }
}
