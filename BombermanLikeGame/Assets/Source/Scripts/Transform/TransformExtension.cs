using UnityEngine;

public static class TransformExtension
{
    public static void SetPos(this Transform transform, Vector3 pos)
    {
        transform.position = pos;
    }

    public static void Move(this Transform transform, Vector3 pos)
    {
        transform.position += pos;
    }

    public static void MoveXZ(this Transform transform, Vector3 pos)
    {
        pos.y = transform.position.y;
        transform.position += pos;
    }

    public static void RotY(this Transform transform, float value)
    {
        var rot = transform.localEulerAngles;
        rot.SetY(value);

        transform.localEulerAngles = rot;
    }
}
