using System;
using UnityEngine;

[Serializable]
public struct MoveComponent
{
    public Transform transform;
    public Rigidbody rigidbody;
    public float speed;

    [field: HideInInspector] public Vector3 direction;
    [field: HideInInspector] public Vector2 gridPosition;
}
