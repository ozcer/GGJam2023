using System;
using System.Collections;
using System.Collections.Generic;
using Oozeling.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : Singleton<ResourceManager>
{
    public float water;
    public float growthWaterCost = 1;
    public List<int> powerups = new List<int>();

    private void Awake()
    {
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

    public void GainPowerup(int mosnterIndex)
    {
        powerups.Add(mosnterIndex);
    }

}
