using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoidUtils {
    private const int NumPoints = 300;
    public static readonly Vector3[] PointsInSphere;

    static BoidUtils (){
        PointsInSphere = new Vector3[NumPoints];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2f;
        float angleIncrement = 2 * Mathf.PI * goldenRatio;

        for (int i = 0; i < NumPoints; i++) {
            float t = i / (float)NumPoints;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Cos(azimuth) * Mathf.Sin(inclination);
            float y = Mathf.Sin(azimuth) * Mathf.Sin(inclination);
            float z = Mathf.Cos(inclination);
            
            PointsInSphere[i] = new Vector3(x, y, z);
        }
    }
}