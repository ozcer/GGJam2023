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
    void Start()
    {
        _cam = Camera.main;
        
        offset = _cam.transform.position - targetPlayerTransform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameInstanceController.Instance.GetGameEnded())
        {
            return;
        }
        
        
        Vector2 center = _cam.pixelRect.position + new Vector2(_cam.pixelWidth / 2, _cam.pixelHeight / 2);

        if (Input.mousePosition.x < _cam.pixelRect.x || Input.mousePosition.x > _cam.pixelRect.x + _cam.pixelWidth || 
            Input.mousePosition.y < _cam.pixelRect.y || Input.mousePosition.y > _cam.pixelRect.y + _cam.pixelHeight)
        {
            return;
        } 

        Vector3 deltaDirection = Input.mousePosition - new Vector3(center.x, center.y, 0);
        if (deltaDirection.magnitude > sufficientDistance)
        {
            deltaDirection -= deltaDirection.normalized * cameraDampenFactor;
            _cam.transform.position += Time.deltaTime * lerpSpeed * deltaDirection;
        }
    }
}
