using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OTween : MonoBehaviour
{
    Vector3 originalPosition;
    Vector3 originalScale;
    
    [Header("Jitter")]
    public bool isJittering = false;
    public Vector2 jitterRange = new Vector2(1f, 1f);
    public float jitterDuration = 0.5f;
    public LeanTweenType jitterEase = LeanTweenType.easeInOutSine;
    
    [Header("Pop")]
    public float popDuration = 0.1f;
    public LeanTweenType popEase = LeanTweenType.easeOutBack;
    public Vector3 popScale = new Vector3(1.2f, 1.2f, 1.2f);
    
    void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;
    }

    public void Jitter(bool force = false)
    {
        if (isJittering && !force) return;
        isJittering = true;
        Vector3 destination = new(
            Random.Range(-jitterRange.x, jitterRange.x), 
            Random.Range(-jitterRange.y, jitterRange.y),
            0f);
        LeanTween.moveLocal(
            gameObject, 
            destination + transform.localPosition, 
            jitterDuration)
            .setEase(jitterEase)
            .setLoopPingPong(1).setOnComplete(() => { Jitter(true);});
    }

    public void Pop()
    {
        LeanTween.scale(gameObject, popScale, popDuration).setEase(popEase).setLoopPingPong(1);
    }

    public void StopAll()
    {
        LeanTween.cancel(gameObject);
        isJittering = false;
        transform.localPosition = originalPosition;
        transform.localScale = originalScale;
        
    }
}
