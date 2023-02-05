using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Resource
{
    bool captured = false;
    public int monsterIndex;
    public override void OnCapture()
    {
        if (captured) return;
        print($"{name} captured");
        captured = true;
        ResourceManager.Instance.GainPowerup(monsterIndex);
        Vector3 sliderWorldPos = Camera.main.ScreenToWorldPoint(HUDController.Instance.waterBarTransform.transform.position);
        CaptureAnimate(sliderWorldPos);

    }
}
