using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oozeling.Helper;
public class SunflowerController : Singleton<SunflowerController>
{
    [SerializeField]
    Animator animator;

    public void Bloom()
    {
        animator.SetTrigger("Bloom");
    }
}
