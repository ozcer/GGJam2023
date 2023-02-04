using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public Vector3 captureAnimationOffset = Vector3.up;
    public float captureAnimationTime = 0.5f;
    public LeanTweenType captureAnimationEase = LeanTweenType.easeOutBounce;
    public abstract void OnCapture();
    
    protected void CaptureAnimate()
    {
        // Bounce up and destroy
        LeanTween.moveLocal(
            gameObject, 
            transform.position + captureAnimationOffset, 
            captureAnimationTime).setEase(captureAnimationEase).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
