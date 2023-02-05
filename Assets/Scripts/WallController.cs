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
  
    public bool PointOutOfBounds(Vector3 point)
    {
        return point.x < leftWallTransform.position.x || point.x > rightWallTransform.position.x ||
            point.y < bottomWallTransform.position.y || point.y > topWallTransform.position.y;
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
