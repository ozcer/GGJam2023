using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    Camera _cam;
    Plane _plane = new(Vector3.forward, 0);
    Vector3 _cursorPos;
    NodeMovementController _selectedNode;
    void Start()
    {
        _cam = Camera.main;
    }
    
    void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow, 1);
        if (_plane.Raycast(ray, out float distance))
        {
            _cursorPos = ray.GetPoint(distance);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _selectedNode = null;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!_selectedNode) _selectedNode = NearestNode(_cursorPos);
            _selectedNode.MoveToward(_cursorPos);
        }
        
    }

    NodeMovementController NearestNode(Vector3 point)
    {
        NodeMovementController[] nodes = FindObjectsOfType<NodeMovementController>();
        NodeMovementController nearest = null;
        float minDistance = float.MaxValue;
        foreach (NodeMovementController node in nodes)
        {
            float distance = Vector3.Distance(point, node.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = node;
            }
        }
        return nearest;
    }
}
