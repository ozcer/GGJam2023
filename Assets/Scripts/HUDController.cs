using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Oozeling.Helper;
using UnityEngine.UI;

public class HUDController : Singleton<HUDController>
{

    [SerializeField]
    GameObject endGameCard;

    [SerializeField]
    GameObject loseScreen;

    [SerializeField]
    GameObject winScreen;

    [SerializeField]
    GameObject titleScreen;

    private float maxLengthValue;

    private Dictionary<int, float> currentLengths = new();
    [SerializeField]
    TextMeshProUGUI lengthIndicatorText;
    
    [Header("Water")]
    public TextMeshProUGUI waterText;
    public RectTransform waterBarTransform;
    public OTween waterSliderTween;
    public SpriteRenderer wateringCanSprite;
    public Vector3 wateringCanTilt = new Vector3(0, 0, 90);
    bool isWatering = false;
    public WateringCan wateringCan;

    ResourceManager resourceManager;
    // Start is called before the first frame update
    public GameObject smokePrefab;
    public Camera seedCamera;
    public Button restartButton;
    public SpriteRenderer seedViewBackground;
    void Start()
    {
        resourceManager = ResourceManager.Instance;
        
        maxLengthValue = GameInstanceController.Instance.Mode.startingMaxLength;
        
        waterSliderTween = waterBarTransform.GetComponent<OTween>();
    }

    void Update()
    {
        // update water UI
        // waterText.text = $"{resourceManager.water} water";
        Vector2 size = waterBarTransform.sizeDelta;
        size.x = resourceManager.water;
        waterBarTransform.sizeDelta = size;
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
        lengthIndicatorText.text = $"{(int)GetCurrentTotalLength()}/{(int)GameInstanceController.Instance.Mode.winLength} cm";
    }

    private IEnumerator ShowWin(int monsterIndex)
    {
        yield return new WaitForSeconds(3);
        // TODO: make this not random
        Instantiate(smokePrefab, SunflowerController.Instance.transform.position, Quaternion.identity);
        MonsterSpawner.Instance.ShowMonster(monsterIndex);
        // tween camera size
        float orthographicSize;
        LeanTween.value((orthographicSize = seedCamera.orthographicSize), orthographicSize - 3, 1f).setEase(LeanTweenType.easeOutBounce).setOnUpdate((val) =>
        {
            seedCamera.orthographicSize = val;
        }).setOnComplete(ShowRestartButton);
        //winScreen.SetActive(true);

    }
    
    public void ShowRestartButton()
    {
        // Send in end game button
        LeanTween.moveLocalY(restartButton.gameObject, restartButton.transform.position.y + 400, 1f)
            .setEase(LeanTweenType.easeOutBounce).setDelay(2f);
    }
    
    public void EndGame(bool won)
    {
        endGameCard.SetActive(true);
        StopWatering();
        if (won)
        {
            // get most common index in  ResourceManager.Instance.powerups
            var most = 0;
            if (ResourceManager.Instance.powerups.Count != 0)
            {
                most = ResourceManager.Instance.powerups.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
                    .Select(grp=>grp.Key).First();
            }
            CamerasController.Instance.ExpandSeedCamera(() => {
                SunflowerController.Instance.Bloom();
                StartCoroutine(ShowWin(most));
            });
        }
        else
        {
            loseScreen.SetActive(true);
            Image r = loseScreen.GetComponent<Image>();
            Image sun = r.transform.GetChild(0).GetComponent<Image>();
            LeanTween.value(gameObject, 0, 1, 2).setOnUpdate((float val) =>
            {
                Color c = r.color;
                c.a = val;
                r.color = c;
                
                Color c2 = sun.color;
                c2.a = val;
                sun.color = c2;
            }).setEase(LeanTweenType.easeInCubic).setOnComplete(ShowRestartButton);
        }

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
