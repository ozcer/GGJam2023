using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public Vector3 captureAnimationScale = Vector3.one * 2;
    public Vector3 captureAnimationOffset = Vector3.up;
    public float captureAnimationTime = 0.5f;
    public LeanTweenType captureAnimationEase = LeanTweenType.easeOutBounce;
    
    public float exitDuration = 0.5f;
    public LeanTweenType exitEase = LeanTweenType.linear;
    public abstract void OnCapture();
    
    protected void CaptureAnimate(Vector3 destination)
    {
        // Bounce up and destroy
        print($"capture animate {name}");
        LeanTween.scale(gameObject, captureAnimationScale, captureAnimationTime).setEase(captureAnimationEase);
        LeanTween.moveLocal(
            gameObject, 
            transform.position + captureAnimationOffset, 
            captureAnimationTime).
            setEase(captureAnimationEase).
            setOnComplete(() =>
            {
                LeanTween.move(
                    gameObject, 
                    destination, 
                    exitDuration).
                    setEase(exitEase).
                    setOnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
            });
    }
}
