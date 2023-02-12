using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Resource
{
    bool captured = false;
    public int waterAmount = 20; // because H20 XDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
    [SerializeField]
    Transform waterSliderTransform;
    [SerializeField]
    SpriteRenderer dropRenderer;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioSource bubbleAudioSource;

    [SerializeField]
    List<AudioClip> audioClips;

    [SerializeField]
    SpriteRenderer backgroundRenderer;

    float drainTime = 1f;

    void Update() {
        Vector3 pos = waterSliderTransform.localPosition;
        pos.x = Mathf.Sin(Time.time);
        waterSliderTransform.localPosition = pos;
    }

    public override void OnCapture()
    {
        if (captured) return;
        print("Water captured");
        captured = true;
        ResourceManager.Instance.GainWater(waterAmount);
        HUDController.Instance.waterSliderTween.StopAll();
        HUDController.Instance.waterSliderTween.Pop();
        
        Vector3 sliderWorldPos = Camera.main.ScreenToWorldPoint(HUDController.Instance.waterBarTransform.transform.position);
        LeanTween.moveLocalY(waterSliderTransform.gameObject, -3, drainTime).setOnComplete(() =>
        {
            dropRenderer.enabled = true;
            CaptureAnimate(sliderWorldPos);
        });
        LeanTween.value(gameObject, backgroundRenderer.color.a, 0, drainTime).setOnUpdate((float a) => {
            Color newCol = backgroundRenderer.color;
            newCol.a = a;
            backgroundRenderer.color = newCol;
        }).setEase(LeanTweenType.easeInCubic);

        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
        audioSource.Play();
        bubbleAudioSource.PlayDelayed(2f);
    }
}
