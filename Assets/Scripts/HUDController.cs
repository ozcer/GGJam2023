using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oozeling.Helper;
public class HUDController : Singleton<HUDController>
{
    [SerializeField]
    private RectTransform sliderRectTransform;

    private float maxLengthValue;

    private Dictionary<int, float> currentLengths = new();
    [SerializeField]
    TextMeshProUGUI lengthIndicatorText;

    float originalMaxSliderSize;
    // Start is called before the first frame update

    void Start()
    {
        maxLengthValue = GameInstanceController.Instance.Mode.startingMaxLength;
        originalMaxSliderSize = sliderRectTransform.sizeDelta.x;
    }

    public bool HasRemainingLength()
    {
        return GetCurrentTotalLength()  < maxLengthValue;
    }

    private float GetCurrentTotalLength()
    {
        float currentTotalLength = 0;
        foreach (float length in currentLengths.Values)
        {
            currentTotalLength += length;
        }
        return currentTotalLength;
    }

    public void SetLength(float newLength, int rootId)
    {
        currentLengths[rootId] = newLength;
        lengthIndicatorText.text = $"{(int)GetCurrentTotalLength()}/{(int)maxLengthValue} cm";
    }

}
