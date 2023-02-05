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

    public bool PointOutOfBounds(Vector3 point)
    {
        return point.x < leftWallTransform.position.x || point.x > rightWallTransform.position.x ||
            point.y < bottomWallTransform.position.y || point.y > topWallTransform.position.y;
    }

}
