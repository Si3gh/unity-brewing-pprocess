using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private bool startOpened;
    [SerializeField] private GameObject firstLayout;
    [SerializeField] private GameObject middleLayout;
    [SerializeField] private GameObject lastLayout;
    [SerializeField] private GameObject parentScene;
    [SerializeField] private TextMeshProUGUI sceneNumber;
    [SerializeField] private GameObject canvas;
#pragma warning restore 0649

    private GameObject[] childScenes ;
    private int currentSceneIndex;
    private int maxNumberOfScenes => childScenes.Length;

    public void Awake()
    {
        canvas.SetActive(startOpened);
        childScenes = GetChildrenScenesInParentScene();
        ShowActualScene();
    }

    public void GoToNextScene()
    {
        HideCurrentScene();
        currentSceneIndex++;
        ShowActualScene();
    }

    public void GoToPreviousScene()
    {
        HideCurrentScene();
        currentSceneIndex--;
        ShowActualScene();
    }

    private void HideCurrentScene()
    {
        childScenes[currentSceneIndex].SetActive(false);
    }

    private void ShowActualScene()
    {
        sceneNumber.text = $"{currentSceneIndex + 1} / {maxNumberOfScenes}";
        childScenes[currentSceneIndex].SetActive(true);
        UpdateButtonLayout();
    }

    private void UpdateButtonLayout()
    {
        if (currentSceneIndex == 0)
        {
            firstLayout.SetActive(true);
            middleLayout.SetActive(false);
            lastLayout.SetActive(false);
        } 
        else if (currentSceneIndex + 1 < maxNumberOfScenes)
        {
            firstLayout.SetActive(false);
            middleLayout.SetActive(true);
            lastLayout.SetActive(false);
        }
        else
        {
            firstLayout.SetActive(false);
            middleLayout.SetActive(false);
            lastLayout.SetActive(true);
        }
    }

    private GameObject[] GetChildrenScenesInParentScene()
    {
        var childrens = new List<GameObject>();
        foreach (Transform children in parentScene.transform)
        {
            var childrenGameObject = children.gameObject;

            childrens.Add(childrenGameObject);
            childrenGameObject.SetActive(false);
        }

        return childrens.ToArray();
    }
}
