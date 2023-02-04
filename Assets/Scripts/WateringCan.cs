using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public GameObject waterPrefab;
    public Transform spawnPoint;
    public void PourWater()
    {
        Instantiate(waterPrefab, spawnPoint.position, Quaternion.identity);
    }
}
