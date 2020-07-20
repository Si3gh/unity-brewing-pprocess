using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    private float fillAmount;

    [SerializeField] 
    private float lerpSpeed;

    [SerializeField] 
    private Image content = null;

    [SerializeField] 
    private Text valueText = null;

    [SerializeField] 
    private Color fullColor;

    [SerializeField] 
    private Color mediumColor;
    
    [SerializeField] 
    private Color lowColor;
    
    public float MaxValue { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Handlebar();
    }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }


    private void Handlebar()
    {
        if (fillAmount != content.fillAmount)
        {
            content.fillAmount = fillAmount;
        }
    }

    private float Map(float Value, float inMin, float inMax, float outMin, float outMax)
    {
        return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        //(80 - 0(dead)) * (1 - 0) / (100 - 0) + 0
        // 80 * 1 / 100
        // 0,8
    }
}
