using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float newNodeSpawnDistance = 1f;
    public float resourceCaptureDistance = 1f;
    public GameObject nodePrefab;
    void FixedUpdate()
    {
        // Check if we should spawn a new node
        if (ShouldSpawnNewNode())
        {
            // Spawn new node
            Instantiate(nodePrefab, transform.position, Quaternion.identity);
        }
        
        Resource resource = GetNearestResource();
        if (resource != null)
        {
            resource.OnCapture();
        }
    }

    bool ShouldSpawnNewNode()
    {
        var colliders = Physics.OverlapSphere(transform.position, newNodeSpawnDistance);
        return colliders.Length == 1;
    }
    

    Resource GetNearestResource()
    {
        var colliders = Physics.OverlapSphere(transform.position, resourceCaptureDistance);
        Resource nearestResource = null;
        float minDistance = float.MaxValue;
        foreach (var collider in colliders)
        {
            var resource = collider.GetComponent<Resource>();
            if (resource != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestResource = resource;
                }
            }
        }
        return nearestResource;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, newNodeSpawnDistance);
    }
    
}
