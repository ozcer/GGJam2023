using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
   public void Start()
   {
      // Fade out
      LeanTween.alpha(gameObject, 0, 2).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => Destroy(gameObject));
   }
}
