using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    // Update is called once per frame
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
