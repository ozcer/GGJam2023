using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    Camera _cam;
    Plane _plane = new(Vector3.forward, 0);
    Vector3 _cursorPos;
    NodeMovementController _selectedNode;

    [SerializeField]
    private GameObject _rootPrefab;

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
        else if (Input.GetMouseButtonDown(0))
        {
            if (!_selectedNode)
            {
                _selectedNode = NearestNode(_cursorPos);
            }
        }

        if (_selectedNode != null)
        {
            _selectedNode.MoveToward(_cursorPos);
        }
        
    }

    NodeMovementController NearestNode(Vector3 point)
    {
        var colliders = Physics.OverlapSphere(point, 5f); // TODO: move magic number
        if (colliders.Length == 0)
        {
            return null;
        }

        Debug.Log(colliders.Length);

        var nearest = colliders[0];
        float minDistanceSquared = (nearest.transform.position - point).sqrMagnitude;
        for (int colliderIndex = 1; colliderIndex < colliders.Length; ++colliderIndex)
        {
            float newDistanceSquared = (colliders[colliderIndex].transform.position - point).sqrMagnitude;
            if (newDistanceSquared < minDistanceSquared)
            {
                minDistanceSquared = newDistanceSquared;
                nearest = colliders[colliderIndex];
            }
        }

        NodeMovementController movementController = nearest.gameObject.GetComponent<NodeMovementController>();
        if (movementController != null) {
            Debug.Log(movementController.gameObject.name);
            return movementController;
        }

        // assume that anything without a movement controller is a node
        GameObject newRoot = Instantiate(_rootPrefab, nearest.transform.position, Quaternion.identity);
        NodeMovementController newMovementController = newRoot.GetComponent<NodeMovementController>();
        return newMovementController;
    }
}
