using TMPro;
using UnityEngine;

public class ErrorWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textField;

    [SerializeField]
    private GameObject window;

    public void ShowError(string errorMessage)
    {
        window.SetActive(true);
        textField.text = errorMessage;
    }
}
