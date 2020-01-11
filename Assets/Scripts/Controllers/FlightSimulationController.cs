using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Flight Simulation Controller.
/// This is the MonoBehaviour Singleton Controller that controls flight logic.
/// </summary>
public class FlightSimulationController : MonoBehaviour
{
    public static FlightSimulationController Instance { get { return Singleton<FlightSimulationController>.Instance; } }

    /// <summary>
    /// On flight finished or interrupted
    /// </summary>
    public UnityEvent FlightFinished;

    /// <summary>
    /// Drone, that need to be controlled
    /// </summary>
    public DroneManager DroneManager;

    /// <summary>
    /// NavMesh of the area
    /// </summary>
    public SpaceNavMesh SpaceNavMesh;

    /// <summary>
    /// Source point marker
    /// </summary>
    public Transform Source;

    /// <summary>
    /// Destination point marker
    /// </summary>
    public Transform Destination;

    /// <summary>
    /// Last calculated path
    /// </summary>
    private Vector3[] path;

    private void Start()
    {
        DroneManager.FlightFinished.AddListener(OnFlightFinished);
    }

    private void OnFlightFinished()
    {
        FlightFinished.Invoke();
    }

    /// <summary>
    /// Start flight from source marker to destination.
    /// </summary>
    public void SimulateFlight()
    {
        path = SpaceNavMesh.FindPath(Source.position, Destination.position);

        if (path == null)
        {
            FlightFinished.Invoke();
            return;
        }

        DroneManager.FlyByPath(path);
    }

    public void CancelFlight()
    {
        DroneManager.CancelFlight();
    }

    private void OnDrawGizmos()
    {
        if (path == null)
            return;

        for (int i = 1; i < path.Length; i++)
        {
            Gizmos.DrawLine(path[i - 1], path[i]);
        }

        foreach (Vector3 point in path)
        {
            Gizmos.DrawSphere(point, 0.25f);
        }
    }
}
