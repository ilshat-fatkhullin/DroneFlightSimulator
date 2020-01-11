using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    Point3DEqualityComparer comparer = new Point3DEqualityComparer();

    HashSet<Point3D> openSet = new HashSet<Point3D>(new Point3DEqualityComparer());
    HashSet<Point3D> closedSet = new HashSet<Point3D>(new Point3DEqualityComparer());

    //cost of start to this key node
    Dictionary<Point3D, int> gScore = new Dictionary<Point3D, int>(new Point3DEqualityComparer());
    //cost of start to goal, passing through key node
    Dictionary<Point3D, int> fScore = new Dictionary<Point3D, int>(new Point3DEqualityComparer());

    Dictionary<Point3D, Point3D> nodeLinks = new Dictionary<Point3D, Point3D>(new Point3DEqualityComparer());

    public List<Point3D> FindPath(bool[,,] graph, Point3D start, Point3D goal)
    {

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, goal);

        //while (openSet.Count > 0)
        for (int i = 0; (i < 2000) && (openSet.Count > 0); i++)
        {
            var current = NextBest();
            if (comparer.Equals(current, goal))
            {
                var result = Reconstruct(start, current);
                return result;
            }


            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbor in Neighbors(graph, current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                var projectedG = GetGScore(current) + 1;

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor);
                else if (projectedG >= GetGScore(neighbor))
                    continue;

                //record it
                nodeLinks[neighbor] = current;
                gScore[neighbor] = projectedG;
                fScore[neighbor] = projectedG + Heuristic(neighbor, goal);

            }
        }

        Debug.Log("Path not found");
        return new List<Point3D>();
    }

    private int Heuristic(Point3D start, Point3D goal)
    {
        var dx = goal.X - start.X;
        var dy = goal.Y - start.Y;
        var dz = goal.Z - start.Z;
        return Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz);
    }

    private int GetGScore(Point3D pt)
    {
        int score = int.MaxValue;
        gScore.TryGetValue(pt, out score);
        return score;
    }


    private int GetFScore(Point3D pt)
    {
        int score = int.MaxValue;
        fScore.TryGetValue(pt, out score);
        return score;
    }

    public static IEnumerable<Point3D> Neighbors(bool[,,] graph, Point3D center)
    {
        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
                for (int z = -1; z < 2; z++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z) != 1)
                        continue;

                    if (IsValidNeighbor(graph, center))
                        yield return new Point3D(center.X + x, center.Y + y, center.Z + z);
                }

        for (int x = -1; x < 2; x++)
            for (int y = -1; y < 2; y++)
                for (int z = -1; z < 2; z++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z) <= 1)
                        continue;

                    if (IsValidNeighbor(graph, center))
                        yield return new Point3D(center.X + x, center.Y + y, center.Z + z);
                }
    }

    public static bool IsValidNeighbor(bool[,,] matrix, Point3D pt)
    {
        int x = pt.X;
        int y = pt.Y;
        int z = pt.Z;
        if (x < 0 || x >= matrix.GetLength(0))
            return false;

        if (y < 0 || y >= matrix.GetLength(1))
            return false;

        if (z < 0 || z >= matrix.GetLength(2))
            return false;

        return matrix[x, y, z];

    }

    private List<Point3D> Reconstruct(Point3D start, Point3D current)
    {
        List<Point3D> path = new List<Point3D>();
        while (nodeLinks.ContainsKey(current))
        {
            path.Add(current);
            current = nodeLinks[current];
        }

        path.Add(start);
        path.Reverse();
        return path;
    }

    private Point3D NextBest()
    {
        int best = int.MaxValue;
        Point3D bestPt = null;
        foreach (var node in openSet)
        {
            var score = GetFScore(node);
            if (score < best)
            {
                bestPt = node;
                best = score;
            }
        }

        return bestPt;
    }

}
