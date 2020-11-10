using UnityEngine;

public abstract class MenuItem : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] protected GameObject Screen;
#pragma warning disable 0649

    public void ShowScreen()
    {
        Screen.gameObject.SetActive(true);
        ShowScreenAction();
    }

    protected abstract void ShowScreenAction();

    public void HideScreen()
    {
        Screen.SetActive(false);
    }
}
