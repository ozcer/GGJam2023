using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oozeling.Helper;
public class HUDController : Singleton<HUDController>
{
    [SerializeField]
    private RectTransform sliderRectTransform;

    [SerializeField]
    private float maxLengthValue = 300;

    private float currentLength = 0;
    [SerializeField]
    TextMeshProUGUI lengthIndicatorText;

    float originalMaxSliderSize;
    // Start is called before the first frame update

    void Start()
    {
        originalMaxSliderSize = sliderRectTransform.sizeDelta.x;
    }

    public bool HasRemainingLength()
    {
        return currentLength < maxLengthValue;
    }

    public void SetLength(float newLength)
    {
        lengthIndicatorText.text = $"{(int)newLength}/{(int)maxLengthValue} cm";
    }

}
