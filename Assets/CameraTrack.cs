using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public float interpolateAmount = 0;
    public int splineNumber = 0;
    public float speed = 20;

    //public SplineSettings splineSettings;
    public Spline spline;
    [HideInInspector]
    public bool splineSettingsFoldout;

    // Start is called before the first frame update
    void Start()
    {
        spline = new Spline(transform.position);

        spline.AddSegment(new Vector3(0, 0, -4f));
        spline.AddSegment(new Vector3(4, 0, .2f));
        spline.AddSegment(new Vector3(3, -2, -.2f));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, speed * Time.deltaTime);
        interpolateAmount += Time.deltaTime * speed / 20;

        /*if(splineNumber < (int) interpolateAmount)
        {
            lastVel = calcVelocity(pointA.position, pointA.anchor, pointB.anchor, pointB.position, 1f);
            lastAccel = calcAccel(pointA.position, pointA.anchor, pointB.anchor, pointB.position, 1f);
        }*/

        splineNumber = (int)interpolateAmount;
        if (splineNumber < spline.CountSegments())
        {
            Vector3 pointA = spline.GetPoint(splineNumber * 3);
            Vector3 pointB = spline.GetPoint(splineNumber * 3 + 1);
            Vector3 pointC = spline.GetPoint(splineNumber * 3 + 2);
            Vector3 pointD = spline.GetPoint(splineNumber * 3 + 3);

            transform.position = polyBez(pointA, pointB, pointC, pointD, interpolateAmount % 1f);
        } else
        {
            interpolateAmount = 0;
        }

        transform.LookAt(Vector3.zero);
    }

    public void OnSplineUpdated()
    {
        //spline = splineSettings.spline;
    }

    Vector3 Lerp(Vector3 x1, Vector3 x2, float t) { // interpolation between x1 and x2 by t
        return Vector3.Lerp(x1, x2, t);
    }

    Vector3 QuadBezier(Vector3 x1, Vector3 c, Vector3 x2, float t) { // interpolation between x1 and c as well as c and x2 by t
        Vector3 a = Lerp(x1, c, t);
        Vector3 b = Lerp(c, x2, t);
        
        return Lerp(a, b, t);
    }

    Vector3 CubicBezier(Vector3 x1, Vector3 c1, Vector3 c2, Vector3 x2, float t) {
        Vector3 a = QuadBezier(x1, c1, c2, t);
        Vector3 b = QuadBezier(c1, c2, x2, t);

        return Lerp(a, b, t);
    }

    // https://youtu.be/aVwxzDHniEw?t=378
    Vector3 polyBez(Vector3 x1, Vector3 c1, Vector3 c2, Vector3 x2, float t)
    {
        Vector3 p0 = x1 * (-Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2) - 3 * t + 1);
        Vector3 p1 = c1 * (3 * Mathf.Pow(t, 3) - 6 * Mathf.Pow(t, 2) + 3 * t);
        Vector3 p2 = c2 * (-3 * Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2));
        Vector3 p3 = x2 * Mathf.Pow(t, 3);

        return p0 + p1 + p2 + p3;
    }

    Vector3 calcVelocity(Vector3 x1, Vector3 c1, Vector3 c2, Vector3 x2, float t)
    {
        Vector3 p0 = x1 * (- 3 *Mathf.Pow(t, 2) + 6 * t - 3);
        Vector3 p1 = c1 * (9 * Mathf.Pow(t, 2) - 12 * t + 3);
        Vector3 p2 = c2 * (-9 * Mathf.Pow(t, 2) + 6 * t);
        Vector3 p3 = x2 * (3 * Mathf.Pow(t, 2));

        return p0 + p1 + p2 + p3;
    }

    Vector3 calcAccel(Vector3 x1, Vector3 c1, Vector3 c2, Vector3 x2, float t)
    {
        Vector3 p0 = x1 * (-6 * t + 6);
        Vector3 p1 = c1 * (18 * t - 12);
        Vector3 p2 = c2 * (-18 * t + 6);
        Vector3 p3 = x2 * (6 * t);

        return p0 + p1 + p2 + p3;
    }
}
