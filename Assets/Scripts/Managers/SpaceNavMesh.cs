using System.Collections.Generic;
using UnityEngine;

public class SpaceNavMesh : MonoBehaviour
{
    public float Width = 20;

    public float Height = 20;

    public float CellSize = 1;

    private bool[,,] environmentMap;

    private void Start()
    {
        int widthCells = Mathf.CeilToInt(Width / CellSize);
        int heightCells = Mathf.CeilToInt(Height / CellSize);
        environmentMap = new bool[widthCells, heightCells, widthCells];
        FillEvironmentMap();
    }

    private void FillEvironmentMap()
    {
        Vector3 halfExtents = Vector3.one * (CellSize / 2);
        for (int x = 0; x < environmentMap.GetLength(0); x++)
            for (int y = 0; y < environmentMap.GetLength(1); y++)
                for (int z = 0; z < environmentMap.GetLength(2); z++)
                {
                    if (Physics.OverlapBox(GetPositionFromCell(new Point3D(x, y, z)), halfExtents).Length == 0)
                    {
                        environmentMap[x, y, z] = true;
                    }
                }
    }

    public Vector3 GetPositionFromCell(Point3D cell)
    {
        return transform.position +
            (Vector3.right * cell.X + Vector3.up * cell.Y + Vector3.forward * cell.Z) * CellSize + 
            Vector3.one * CellSize / 2;
    }

    public Point3D GetCellFromPosition(Vector3 position)
    {
        Vector3 originPosition = position - transform.position;
        return new Point3D
        (
            Mathf.FloorToInt(originPosition.x / CellSize),
            Mathf.FloorToInt(originPosition.y / CellSize),
            Mathf.FloorToInt(originPosition.z / CellSize)
        );
    }

    public Vector3[] FindPath(Vector3 source, Vector3 destination)
    {
        Point3D sourceCell = GetClosestFreePoint(source);
        Point3D destinationCell = GetClosestFreePoint(destination);

        AStarPathfinding aStarPathfinding = new AStarPathfinding();
        List<Point3D> path = aStarPathfinding.FindPath(environmentMap, sourceCell, destinationCell);
        Vector3[] result = new Vector3[path.Count];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = GetPositionFromCell(path[i]);
        }

        return result;
    }

    private Point3D GetClosestFreePoint(Vector3 position)
    {
        Point3D point = GetCellFromPosition(position);
        while (point.Y < environmentMap.GetLength(1) - 1 &&
            !environmentMap[point.X, point.Y, point.Z])
        {
            point.Y++;
        }

        return point;
    }

    public Vector3 GetClosestFreePosition(Vector3 position)
    {
        return GetPositionFromCell(GetClosestFreePoint(position));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3.forward * Width + Vector3.right * Width + Vector3.up * Height) / 2, new Vector3(Width, Height, Width));

        if (environmentMap == null)
            return;

        Vector3 boxSize = Vector3.one * CellSize;

        Gizmos.color = Color.red;

        for (int x = 0; x < environmentMap.GetLength(0); x++)
            for (int y = 0; y < environmentMap.GetLength(1); y++)
                for (int z = 0; z < environmentMap.GetLength(2); z++)
                {
                    if (!environmentMap[x, y, z])
                    {
                        Gizmos.DrawCube(GetPositionFromCell(new Point3D(x, y, z)), boxSize);
                    }
                }
    }
}
