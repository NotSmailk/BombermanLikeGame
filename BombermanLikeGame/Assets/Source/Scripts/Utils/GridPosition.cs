using UnityEngine;

public class GridPosition
{
    public static Vector3 GetPosition(int r, int rows, int c, int cols, float offset)
    {
        var x = c - cols / 2f + offset;
        var y = 0.01f;
        var z = r - rows / 2f + offset;
        return new Vector3(x, y, -z);
    }
}
