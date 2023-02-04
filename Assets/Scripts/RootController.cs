using UnityEngine;

public class RootController : MonoBehaviour
{
    private class RootControllerIdGenerator
    {
        private static int m_autoIncrementId = 0;
        public static int AutoIncrementId
        {
            get
            {
                return m_autoIncrementId++;
            }
        }
    }

    [SerializeField]
    private TrailRenderer trailRenderer;
    AnimationCurve originalWidthCurve;
    float originalTaperKeyframeTime;

    const int taperKeyFrameIndex = 1;
    const int scaleAfterCount = 6;
    private int rootId;

    void Start()
    {
        rootId = RootControllerIdGenerator.AutoIncrementId;
        originalWidthCurve = trailRenderer.widthCurve;
        originalTaperKeyframeTime = originalWidthCurve.keys[taperKeyFrameIndex].time;
    }

    void Update()
    {
        float fullDistance = GetCurrentDistance();
        // TODO: check if reached target and trigger win condition
        HUDController.Instance.SetLength(fullDistance, rootId);

        if (trailRenderer.positionCount > scaleAfterCount)
        {
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

    float GetCurrentDistance()
    {
        float fullDistance = 0;
        if (trailRenderer.positionCount > 2)
        {
            fullDistance += (trailRenderer.positionCount - 1) * trailRenderer.minVertexDistance;
            fullDistance += (trailRenderer.GetPosition(trailRenderer.positionCount - 1) - trailRenderer.GetPosition(trailRenderer.positionCount - 2)).magnitude;
        }
        else if (trailRenderer.positionCount == 0)
        {
            return 0;
        }
        else
        {
            fullDistance = (trailRenderer.GetPosition(0) - transform.position).magnitude;
        }
        return fullDistance;
    }
}
