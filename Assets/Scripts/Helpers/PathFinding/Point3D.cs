using System.Collections.Generic;
using UnityEngine;

public class Point3D
{
    public int X, Y, Z;
    public Point3D(int x, int y, int z) { X = x; Y = y; Z = z; }
    public Point3D(float x, float y, float z) { X = (int)x; Y = (int)y; Z = (int)z; }
    public Point3D(Vector3 v3) { X = (int)v3.x; Y = (int)v3.y; Z = (int)v3.z; }
}

public class Point3DEqualityComparer : EqualityComparer<Point3D>
{
    public override bool Equals(Point3D x, Point3D y)
    {
        return x.X == y.X && x.Y == y.Y && x.Z == y.Z;
    }

    public override int GetHashCode(Point3D obj)
    {
        return base.GetHashCode();
    }
}
