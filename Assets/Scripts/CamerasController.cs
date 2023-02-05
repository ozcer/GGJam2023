using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oozeling.Helper;
public class CamerasController : Singleton<CamerasController>
{
    [SerializeField]
    Camera seedCamera;
    
    public void ExpandSeedCamera(System.Action callback)
    {
        LeanTween.value(seedCamera.gameObject, 0.3f, 1f, 1f).setEase(LeanTweenType.easeInBack).setOnUpdate((float val) => {
            Rect normalizedCameraOnScreenRect = seedCamera.rect;
            normalizedCameraOnScreenRect.width = val;
            seedCamera.rect = normalizedCameraOnScreenRect;
        }).setOnComplete(callback);
    }
}
