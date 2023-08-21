using System;
using UnityEngine;

[Serializable]
public struct TileComponent
{
    public Transform transform;
    public Vector2 gridPosition;
    public TileState tileState;
}
