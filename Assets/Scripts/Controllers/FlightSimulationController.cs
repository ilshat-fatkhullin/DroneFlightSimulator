using UnityEngine;

public class FlightSimulationController : MonoBehaviour
{
    public static FlightSimulationController Instance { get { return Singleton<FlightSimulationController>.Instance; } }

    public SpaceNavMesh SpaceNavMesh;

    public Transform Source;

    public Transform Destination;

    private Vector3[] path;

    public void SimulateFlight()
    {
        path = SpaceNavMesh.FindPath(Source.position, Destination.position);
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
