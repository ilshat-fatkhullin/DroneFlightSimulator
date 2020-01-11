using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneManager : MonoBehaviour
{
    #region PUBLIC FIELDS

    public UnityEvent FlightFinished;

    public Transform ModelRoot;

    public float LandingOffset;

    public float Speed;

    #endregion

    #region PRIVATE FIEDLS

    private SplineBuilder splineBuilder;

    private float flightStartTime;

    #endregion

    #region PUBLIC METHODS

    public void FlyByPath(Vector3[] path)
    {
        splineBuilder = new SplineBuilder(path);
        gameObject.SetActive(true);
        flightStartTime = Time.time;
    }

    public void CancelFlight()
    {
        FlightFinished.Invoke();
        gameObject.SetActive(false);
    }

    #endregion

    #region PRIVATE METHODS

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private Vector3 GetGroundedPoint(Vector3 point)
    {
        RaycastHit hitResult;
        if (Physics.Raycast(point, Vector3.down, out hitResult))
        {
            return hitResult.point + Vector3.up * LandingOffset;
        }
        return point;
    }

    private void Update()
    {
        float t = (Time.time - flightStartTime) * Speed;

        if (splineBuilder.IsFinishedAtTime(t))
        {
            CancelFlight();
            return;
        }

        Vector3 lookDirection = splineBuilder.GetSpeedAtTime(t).normalized;
        lookDirection.y = 0;

        transform.position = splineBuilder.GetPositionAtTime(t);
        ModelRoot.rotation = Quaternion.Lerp(ModelRoot.rotation,
            Quaternion.LookRotation(lookDirection),
            Time.deltaTime);
    }

    #endregion
}
