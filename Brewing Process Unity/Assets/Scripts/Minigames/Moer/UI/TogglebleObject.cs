using UnityEngine;

public class TogglebleObject : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject activeImage;
    [SerializeField] private GameObject inactiveImage;
    [SerializeField] private bool isActive;
#pragma warning restore 0649

    public void Activate()
    {
        activeImage.SetActive(true);
        inactiveImage.SetActive(false);
        isActive = true;
    }

    public void Inactivate()
    {
        activeImage.SetActive(false);
        inactiveImage.SetActive(true);
        isActive = false;
    }

    public void Toggle()
    {
        isActive = !isActive;
        activeImage.SetActive(isActive);
        inactiveImage.SetActive(!isActive);
    }
}