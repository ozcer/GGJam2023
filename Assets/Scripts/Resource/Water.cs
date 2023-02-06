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
    List<AudioClip> audioClips;
    public override void OnCapture()
    {
        if (captured) return;
        print("Water captured");
        captured = true;
        ResourceManager.Instance.GainWater(waterAmount);
        HUDController.Instance.waterSliderTween.StopAll();
        HUDController.Instance.waterSliderTween.Pop();
        
        Vector3 sliderWorldPos = Camera.main.ScreenToWorldPoint(HUDController.Instance.waterBarTransform.transform.position);
        LeanTween.moveLocalY(waterSliderTransform.gameObject, -20, 1f).setOnComplete(() =>
        {
            dropRenderer.enabled = true;
            CaptureAnimate(sliderWorldPos);
        });
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Count)];
        audioSource.Play();
    }
}
