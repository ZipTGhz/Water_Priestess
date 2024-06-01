using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : CustomMonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private TextMeshProUGUI _text;

    //Default is Fraction
    public TextMode TextMode = TextMode.Fraction;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        _slider = GetComponent<Slider>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
        SetText(TextMode);
    }

    public void SetValue(float currentValue)
    {
        _slider.value = currentValue;
        SetText(TextMode);
    }

    private void SetText(TextMode textMode)
    {
        float value = _slider.value;
        float maxValue = _slider.maxValue;

        Debug.Log(value + " " + maxValue);

        switch (textMode)
        {
            case TextMode.Fraction:
                _text.SetText(value.ToString("F2") + "/" + maxValue.ToString());
                break;
            case TextMode.Percentage:
                _text.SetText((value / maxValue * 100).ToString("F2") + "%");
                break;
        }
    }
}

public enum TextMode
{
    Fraction,
    Percentage,
}
