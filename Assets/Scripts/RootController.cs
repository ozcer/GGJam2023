using UnityEngine;

// Tracks root length, but not responsible for movement. See InputController for that
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
    [SerializeField]
    bool isDecorative;

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
    float previousFullDistance = 0;
    void Update()
    {
        float fullDistance = GetCurrentDistance();
        if (isDecorative)
        {
            return;
        }
        if (HUDController.Instance.GetCurrentTotalLength() >= GameInstanceController.Instance.Mode.winLength)
        {
            if (!GameInstanceController.Instance.GetGameEnded())
            {
                HUDController.Instance.EndGame(true);
            }
            return;
        }
        // TODO: check if reached target and trigger win condition
        HUDController.Instance.SetLength(fullDistance, rootId);
        float delta = fullDistance - previousFullDistance;
        previousFullDistance = fullDistance;
        if (delta > 0.0001f)
        {
            ResourceManager.Instance.GrowPlant(delta);

        }
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
