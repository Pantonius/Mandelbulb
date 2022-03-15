using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spline")]
public class Spline : ScriptableObject
{
    [SerializeField] public List<Point> pointList;
    [SerializeField] public List<Anchor> anchorList;

    public int Length()
    {
        return pointList.Count;
    }

    public Point GetPoint(int index)
    {
        return pointList[index];
    }

    public Anchor GetAnchor(int index)
    {
        return anchorList[index];
    }

    [Serializable]
    public class Point
    {
        public float t;
        public Vector3 position;
        public Vector3 forward;
        public Vector3 normal;
    }

    [Serializable]
    public class Anchor
    {
        public Vector3 position;
    }
}
