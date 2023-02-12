using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oozeling.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ResourceManager : Singleton<ResourceManager>
{
    public float water;
    public float growthWaterCost = 1;
    List<int> powerups = new List<int>();
    AudioSource pickupEffectAudioSource;
    private void Awake()
    {
        pickupEffectAudioSource = GetComponent<AudioSource>();
        water = (int) GameInstanceController.Instance.Mode.startingMaxLength / growthWaterCost;
    }

    public void GrowPlant(float deltaDistance)
    {
        if (GameInstanceController.Instance.GetGameEnded())
        {
            return;
        }
        water -= deltaDistance / growthWaterCost;
        if (water <= 0)
        {
            HUDController.Instance.EndGame(false);
        }
    }
    
    public void GainWater(int amount)
    {
        water += amount;
    }

    public void GainPowerup(int monsterIndex)
    {
        powerups.Add(monsterIndex);
        pickupEffectAudioSource.Play();
    }

    public int GetMonsterIndex() {
        if (powerups.Count == 0) {
            return 0;
        }
        return powerups.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
                    .Select(grp=>grp.Key).First();
    }

}
