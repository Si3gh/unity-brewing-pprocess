using UnityEngine;

public class TopMenu : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject ClosedScene;
    [SerializeField] private GameObject HalfOpenedScene;
    [SerializeField] private GameObject OpenedScene;
    [SerializeField] private MenuItem[] OpenedChildScenes;
#pragma warning restore 0649

    public void CloseScene()
    {
        HideScenes();
        ClosedScene.SetActive(true);
    }

    public void ShowHalfScene()
    {
        HideScenes();
        HalfOpenedScene.SetActive(true);
    }

    public void OpenScene(int sceneIndex)
    {
        HideScenes();
        OpenedScene.SetActive(true);
        
        foreach (var scene in OpenedChildScenes)
        {
            scene.gameObject.SetActive(false);
        }

        OpenedChildScenes[sceneIndex].ShowScreen();
    }

    private void HideScenes()
    {
        ClosedScene.SetActive(false);
        HalfOpenedScene.SetActive(false);
        OpenedScene.SetActive(false);
    }
}
