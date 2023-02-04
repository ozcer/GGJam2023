using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeMovementController : MonoBehaviour
{
    // Update is called once per frame
    public float speed = 1f;
    public void MoveToward(Vector3 pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
    }
}
