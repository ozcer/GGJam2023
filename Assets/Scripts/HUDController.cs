using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oozeling.Helper;
using UnityEngine.UI;

public class HUDController : Singleton<HUDController>
{
    [SerializeField]
    private RectTransform sliderRectTransform;

    [SerializeField]
    GameObject endGameCard;

    [SerializeField]
    TextMeshProUGUI endGameText;

    private float maxLengthValue;

    private Dictionary<int, float> currentLengths = new();
    [SerializeField]
    TextMeshProUGUI lengthIndicatorText;
    
    [Header("Water")]
    public TextMeshProUGUI waterText;
    public Slider waterSlider;
    public OTween waterSliderTween;
    
    float originalMaxSliderSize;
    ResourceManager resourceManager;
    // Start is called before the first frame update

    void Start()
    {
        resourceManager = ResourceManager.Instance;
        
        maxLengthValue = GameInstanceController.Instance.Mode.startingMaxLength;
        originalMaxSliderSize = sliderRectTransform.sizeDelta.x;
        
        waterSliderTween = waterSlider.GetComponent<OTween>();
    }

    void Update()
    {
        // update water UI
        // waterText.text = $"{resourceManager.water} water";
        waterSlider.value = (float) resourceManager.water / resourceManager.maxWater;
    }

    public bool HasRemainingLength()
    {
        return GetCurrentTotalLength()  < maxLengthValue;
    }

    public float GetCurrentTotalLength()
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


    public void EndGame(bool won)
    {
        endGameCard.SetActive(true);
        endGameText.text = won ? "The plant grew :D" : "The plant died :(";
        GameInstanceController.Instance.EndGame();
    }
    
}
