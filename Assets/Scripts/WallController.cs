using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField]
    Transform bottomWallTransform;
    [SerializeField]
    Transform topWallTransform;
    [SerializeField]
    Transform leftWallTransform;
    [SerializeField]
    Transform rightWallTransform;

    List<Transform> wallTransformList = new List<Transform>();
  
    public Vector3 BoundPointInsideWalls(Vector3 point)
    {
        point.x = Mathf.Clamp(point.x, leftWallTransform.position.x, rightWallTransform.position.x);
        point.y = Mathf.Clamp(point.y, bottomWallTransform.position.y, topWallTransform.position.y);
        return point;
    }

    void OnDrawGizmos()
    {
        if (wallTransformList.Count == 0)
        {
            wallTransformList = new List<Transform>() { bottomWallTransform, topWallTransform, leftWallTransform, rightWallTransform };
        }
        Gizmos.color = Color.green;
        foreach (Transform wallTransfrom in wallTransformList)
        {
            Gizmos.DrawWireCube(wallTransfrom.position, wallTransfrom.localScale);
        }
    }
}
