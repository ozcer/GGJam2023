using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Resource
{
    public int monsterIndex;
    public override bool OnCapture()
    {
        if (base.OnCapture()) {
            return true;
        }
        ResourceManager.Instance.GainPowerup(monsterIndex);
        Vector3 sliderWorldPos = Camera.main.ScreenToWorldPoint(HUDController.Instance.waterBarTransform.transform.position);
        CaptureAnimate(sliderWorldPos);
        return false;
    }
}
