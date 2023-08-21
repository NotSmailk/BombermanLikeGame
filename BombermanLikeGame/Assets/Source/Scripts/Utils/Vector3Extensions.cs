using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static void SetX(this Vector3 pos, float xValue)
    {
        var newPos = pos;
        newPos.x = xValue;
        pos = newPos;
    }

    public static void SetY(this Vector3 pos, float yValue)
    {
        var newPos = pos;
        newPos.y = yValue;
        pos = newPos;
    }

    public static void SetZ(this Vector3 pos, float zValue)
    {
        var newPos = pos;
        newPos.z = zValue;
        pos = newPos;
    }

    public static Vector3 Random(this List<Vector3> v)
    {
        if (v == null || v.Count <= 0)
            return Vector3.zero;

        int i = UnityEngine.Random.Range(0, v.Count);
        return v[i];
    }
}
