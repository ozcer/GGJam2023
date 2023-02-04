using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Resource
{
    bool _captured = false;
    public override void OnCapture()
    {
        if (_captured) return;
        print("Water captured");
        _captured = true;
        CaptureAnimate();
    }
}
