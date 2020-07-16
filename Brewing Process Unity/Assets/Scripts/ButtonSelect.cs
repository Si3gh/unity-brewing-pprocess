using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    private Image image;
    private bool isActive = false;
    private float fillAmmount = 0;
    private float fillSpeed = 2;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void Enable()
    {
        if (image != null)
        {
            isActive = true;
            image.fillAmount = 0;
            fillAmmount = 0;
        }
    }

    public void Disable()
    {
        if (image != null)
        {
            isActive = false;
            image.fillAmount = 0;
        }
    }

    public void Update()
    {
        if (isActive && fillAmmount < 1)
        {
            fillAmmount += Time.deltaTime * fillSpeed;
            image.fillAmount = Mathf.Clamp(fillAmmount, 0, 1);
        }
    }
}
