using TMPro;
using UnityEngine;

namespace Request
{
    public class ErrorWindow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textField = null;

        [SerializeField]
        private GameObject window = null;

        public void ShowError(string errorMessage)
        {
            window.SetActive(true);
            textField.text = "Parece que houve um erro";

            if (errorMessage.Length != 0)
            {
                textField.text = errorMessage;
            }
        }
    }
}
