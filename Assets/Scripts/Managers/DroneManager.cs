using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneManager : MonoBehaviour
{
    public UnityEvent FlightFinished;

    public Transform ModelRoot;

    public float LandingOffset;

    public float Speed;

    private SplineBuilder splineBuilder;

    private float flightStartTime;    

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void FlyByPath(Vector3[] path)
    {
        List<Vector3> points = new List<Vector3>(path);

        points.Insert(0, GetGroundedPoint(path[0]));
        points.Add(GetGroundedPoint(path[path.Length - 1]));

        splineBuilder = new SplineBuilder(points.ToArray());
        gameObject.SetActive(true);
        flightStartTime = Time.time;
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

    public void CancelFlight()
    {
        FlightFinished.Invoke();
        gameObject.SetActive(false);
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
}
