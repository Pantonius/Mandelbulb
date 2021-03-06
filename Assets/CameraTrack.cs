using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public float interpolateAmount = 0;
    public int splineNumber = 0;
    public float speed = 20;

    //public SplineSettings splineSettings;
    [HideInInspector]
    public Spline spline;
    [HideInInspector]
    public bool splineSettingsFoldout;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, speed * Time.deltaTime);
        interpolateAmount += Time.deltaTime * speed / 20;

        splineNumber = (int)interpolateAmount;
        if (splineNumber + 1 < spline.Length())
        {
            Spline.Point pointA = spline.GetPoint(splineNumber);
            Spline.Anchor anchorA = spline.GetAnchor(splineNumber);

            Spline.Point pointB = spline.GetPoint(splineNumber + 1);
            Spline.Anchor anchorB = spline.GetAnchor(splineNumber + 1);


            transform.position = cubicBezier(pointA.position, anchorA.position, anchorB.position, pointB.position, interpolateAmount % 1f);
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

    Vector3 linearBezier(Vector3 x1, Vector3 x2, float t) { // interpolation between x1 and x2 by t
        return Vector3.Lerp(x1, x2, t);
    }

    Vector3 quadBezier(Vector3 x1, Vector3 x2, Vector3 c, float t) { // interpolation between x1 and c as well as c and x2 by t
        Vector3 a = linearBezier(x1, c, t);
        Vector3 b = linearBezier(c, x2, t);
        
        return linearBezier(a, b, t);
    }

    Vector3 cubicBezier(Vector3 x1, Vector3 x2, Vector3 c1, Vector3 c2, float t) {
        Vector3 a = quadBezier(x1, c1, c2, t);
        Vector3 b = quadBezier(c1, c2, x2, t);

        return linearBezier(a, b, t);
    }

    // https://youtu.be/aVwxzDHniEw?t=378
    Vector3 polyBez(Vector3 x1, Vector3 x2, Vector3 c1, Vector3 c2, float t)
    {
        Vector3 p0 = x1 * (-Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2) - 3 * t + 1);
        Vector3 p1 = c1 * (3 * Mathf.Pow(t, 3) - 6 * Mathf.Pow(t, 2) + 3 * t);
        Vector3 p2 = c2 * (-3 * Mathf.Pow(t, 3) + 3 * Mathf.Pow(t, 2));
        Vector3 p3 = x2 * Mathf.Pow(t, 3);

        return p0 + p1 + p2 + p3;
    }

    Vector3 calcVelocity(Vector3 x1, Vector3 x2, Vector3 c1, Vector3 c2, float t)
    {
        Vector3 p0 = x1 * (- 3 *Mathf.Pow(t, 2) + 6 * t - 3);
        Vector3 p1 = c1 * (9 * Mathf.Pow(t, 2) - 12 * t + 3);
        Vector3 p2 = c2 * (-9 * Mathf.Pow(t, 2) + 6 * t);
        Vector3 p3 = x2 * (3 * Mathf.Pow(t, 2));

        return p0 + p1 + p2 + p3;
    }
}
