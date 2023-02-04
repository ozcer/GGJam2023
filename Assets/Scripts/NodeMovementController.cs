using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeMovementController : MonoBehaviour
{
    // Update is called once per frame
    Camera _cam;
    Plane _plane = new(Vector3.forward, 0);
    Vector3 _cursorPos;
    public float speed = 1f;
    void Start()
    {
        _cam = Camera.main;
    }

    void FixedUpdate()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow, 5);
        if (_plane.Raycast(ray, out float distance))
        {
            _cursorPos = ray.GetPoint(distance);
        }
        
        if (Input.GetMouseButton(0))
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _cursorPos, step);
        }
    }
}
