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
    public SpriteRenderer wateringCanSprite;
    public Vector3 wateringCanTilt = new Vector3(0, 0, 90);
    bool isWatering = false;
    public WateringCan wateringCan;

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
    
    public void StartWatering()
    {
        if (isWatering) return;
        isWatering = true;
        
        // waterSliderTween.Jitter();
        wateringCanSprite.GetComponent<OTween>().Jitter(OnComplete: () =>
        {
            wateringCan.PourWater();
        });
        LeanTween.rotateLocal(
            wateringCanSprite.gameObject, 
            wateringCanTilt, 0.5f).
            setEase(LeanTweenType.easeInOutQuad).
            setLoopPingPong(1).setOnComplete(() =>
            {
                isWatering = false;
            });
    }
    
    public void StopWatering()
    {
        wateringCanSprite.transform.localRotation = Quaternion.identity;
        wateringCanSprite.GetComponent<OTween>().StopAll();
        isWatering = false;
    }
    
}
