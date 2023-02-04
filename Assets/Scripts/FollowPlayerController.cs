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
        Vector2 center = _cam.pixelRect.position + new Vector2(_cam.pixelWidth / 2, _cam.pixelHeight / 2);


        //Debug.DrawRay(center.origin, ray.origin - center.origin, Color.red, 10);

        //Vector3 deltaDirection = ray.origin - center.origin;
        Vector3 deltaDirection = Input.mousePosition - new Vector3(center.x, center.y, 0);
        Debug.Log(" Center" + center + " input" + Input.mousePosition);
        Debug.Log("Magnitude: " + deltaDirection.magnitude);
        if (deltaDirection.magnitude > sufficientDistance)
        {
            deltaDirection -= deltaDirection.normalized * cameraDampenFactor;
            _cam.transform.position += Time.deltaTime * lerpSpeed * deltaDirection;
        }
    }
}
