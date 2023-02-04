using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float newNodeSpawnDistance = 1f;
    public GameObject nodePrefab;
    void FixedUpdate()
    {
        // Get all nodes in radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, newNodeSpawnDistance);
        
        // Found only self in radius
        if (hitColliders.Length == 1)
        {
            // Spawn new node
            Instantiate(nodePrefab, transform.position, Quaternion.identity);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, newNodeSpawnDistance);
    }
    
}
