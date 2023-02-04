using System;
using System.Collections;
using System.Collections.Generic;
using Oozeling.Helper;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : Singleton<ResourceManager>
{
    public int water = 100;
    public int maxWater = 100;
    public int growthWaterCost = 1;

    public void GrowPlant()
    {
        if (water >= growthWaterCost)
        {
            water -= growthWaterCost;
        }
    }
    
    public void GainWater(int amount)
    {
        water += amount;
        if (water > maxWater)
        {
            water = maxWater;
        }
    }
    
}
