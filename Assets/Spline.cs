using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spline
{
    [SerializeField] public List<Vector3> points;

    public Spline(Vector3 centre)
    {
        points = new List<Vector3>
        {
            centre + Vector3.left,
            centre + (Vector3.left + Vector3.up) * .5f,
            centre + (Vector3.right + Vector3.down) * .5f,
            centre + Vector3.right
        };
    }

    public int Length()
    {
        return points.Count;
    }

    public int CountSegments()
    {
        return (Length() - 4) / 3 + 1;
    }

    public Vector3 GetPoint(int index)
    {
        return points[index];
    }
    public void AddSegment(Vector3 pos)
    {
        points.Add(points[Length() - 1] * 2 - points[Length() - 2]); // Control
        points.Add((points[Length() - 1] + pos) * .5f); // Control
        points.Add(pos); // Point
    }

    /*[Serializable]
    public class Point
    {
        public Vector3 position;
        public Vector3 anchor;
    }*/
}
