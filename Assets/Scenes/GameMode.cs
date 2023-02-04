using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameMode", order = 1)]
public class GameMode : ScriptableObject
{
    [SerializeField]
    public float startingMaxLength;

    [SerializeField]
    public float winLength;

    [SerializeField]
    public int timeLimitSeconds;
}
