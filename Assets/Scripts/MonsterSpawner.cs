using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oozeling.Helper;
public class MonsterSpawner : Singleton<MonsterSpawner>
{
    [SerializeField]
    List<GameObject> Monsters;

    [SerializeField]
    List<Sprite> Signs;

    [SerializeField]
    SpriteRenderer spriteRenderer; 

    [SerializeField]
    ParticleSystem Smoke;

    public void ShowMonster(int index)
    {
        if (index != 0)
        {
            SunflowerController.Instance.gameObject.SetActive(false);
        }
        if (Monsters[index] != null)
        {
            GameObject result = Instantiate(Monsters[index], transform);
            result.transform.localPosition = Vector3.zero;
            result.transform.localRotation = Quaternion.identity;
            result.transform.localScale = Vector3.one;
        }
        spriteRenderer.sprite = Signs[index];
    }
}
