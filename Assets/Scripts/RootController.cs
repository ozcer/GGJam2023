using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer trailRenderer;
    AnimationCurve originalWidthCurve;
    float originalTaperKeyframeTime;

    const int taperKeyFrameIndex = 1;
    const int scaleAfterCount = 6;

    void Start()
    {
        originalWidthCurve = trailRenderer.widthCurve;
        originalTaperKeyframeTime = originalWidthCurve.keys[taperKeyFrameIndex].time;
    }

    void Update()
    {
        if (trailRenderer.positionCount > scaleAfterCount)
        {
            float fullDistance = 0;
            if (trailRenderer.positionCount > 2)
            {
                fullDistance += (trailRenderer.positionCount - 1) * trailRenderer.minVertexDistance;
                fullDistance += (trailRenderer.GetPosition(trailRenderer.positionCount - 1) - trailRenderer.GetPosition(trailRenderer.positionCount - 2)).magnitude;
            }
            else
            {
                fullDistance = (trailRenderer.GetPosition(0) - transform.position).magnitude;
            }

            float intendedDistance = scaleAfterCount * trailRenderer.minVertexDistance;
            float newTime = intendedDistance * originalTaperKeyframeTime / fullDistance;
            AnimationCurve newCurve = originalWidthCurve;
            Keyframe keyframe = newCurve.keys[taperKeyFrameIndex];
            keyframe.time = newTime;
            newCurve.MoveKey(taperKeyFrameIndex, keyframe);
            trailRenderer.widthCurve = newCurve;
            //Debug.Log("TIME: " + trailRenderer.widthCurve.keys[1].time);
        }
    }
}
