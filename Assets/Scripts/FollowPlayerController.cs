using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
public class FollowPlayerController : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;
    Plane _plane = new(Vector3.forward, 0);
    [SerializeField]
    private Transform targetPlayerTransform;
    private Vector3 offset;

    [SerializeField]
    private float sufficientDistance;

    [Tooltip("Higher value slows down camera more when it's almost at the player")]
    [SerializeField]
    private float cameraDampenFactor;

    Camera _cam;

    [SerializeField]
    Material rockMat;

    Vector2 screenSizeInWorldUnits;

    WallController wallController;
    void Start()
    {
        wallController = FindAnyObjectByType<WallController>();
        Assert.IsNotNull(wallController);
        _cam = Camera.main;
        
        offset = _cam.transform.position - targetPlayerTransform.position;

        Ray leftRay = _cam.ViewportPointToRay(Vector2.zero);
        _plane.Raycast(leftRay, out float distanceMouse);
        Vector3 bottomLeftCornerWorld = leftRay.GetPoint(distanceMouse);

        Ray rightRay = _cam.ViewportPointToRay(Vector2.one);
        _plane.Raycast(rightRay, out float distanceCenter);
        Vector3 topRightCornerWorld = rightRay.GetPoint(distanceCenter);

        screenSizeInWorldUnits = topRightCornerWorld - bottomLeftCornerWorld;
        Debug.LogWarning(screenSizeInWorldUnits);
        Debug.LogWarning(_cam.pixelRect);
    }

    Vector2 totalDelta = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        if (GameInstanceController.Instance.GetGameEnded())
        {
            return;
        }
        
        // assumes 0.3 for plant camera
        Vector2 center = _cam.pixelRect.position + new Vector2(0.65f * _cam.pixelWidth, _cam.pixelHeight / 2); // TODO: remove magic number
        Vector3 mousePosInScreenUnits = Input.mousePosition;
        if (mousePosInScreenUnits.x < _cam.pixelRect.x + _cam.pixelWidth * 0.3f || mousePosInScreenUnits.x > _cam.pixelRect.x + _cam.pixelWidth || 
            mousePosInScreenUnits.y < _cam.pixelRect.y || mousePosInScreenUnits.y > _cam.pixelRect.y + _cam.pixelHeight)
        {
            return;
        }
        Ray mouseRay = _cam.ScreenPointToRay(mousePosInScreenUnits);
        _plane.Raycast(mouseRay, out float distance);
        Vector3 worldPos = mouseRay.GetPoint(distance);
        worldPos = wallController.BoundPointInsideWalls(worldPos);
        mousePosInScreenUnits = _cam.WorldToScreenPoint(worldPos);


        Vector3 deltaDirectionInScreenUnits = mousePosInScreenUnits - new Vector3(center.x, center.y, 0);
        
       
        if (deltaDirectionInScreenUnits.magnitude > sufficientDistance)
        {
            deltaDirectionInScreenUnits -= deltaDirectionInScreenUnits.normalized * cameraDampenFactor;
            deltaDirectionInScreenUnits *= Time.deltaTime* lerpSpeed;

            // transform in world
            float screenToWorldUnits = screenSizeInWorldUnits.y / _cam.pixelHeight;
            Vector3 deltaDirectionInWorldUnits = deltaDirectionInScreenUnits * screenToWorldUnits;
            _cam.transform.position += deltaDirectionInWorldUnits;

            // calculate texture offset
            float screenToRockTextureUnits = 1 / (float)_cam.pixelHeight;
            Vector2 textureDifference = new Vector2(deltaDirectionInScreenUnits.x * screenToRockTextureUnits, deltaDirectionInScreenUnits.y * screenToRockTextureUnits);
            totalDelta += textureDifference;
            rockMat.SetTextureOffset("_MainTex", totalDelta);
            // Debug.Log(totalDelta);
        }
    }
}
