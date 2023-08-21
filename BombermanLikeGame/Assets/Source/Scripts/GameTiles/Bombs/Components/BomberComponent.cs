using System;
using UnityEngine;

[Serializable]
public struct BomberComponent
{
    [field: HideInInspector] public int bombLevel;
    [field: HideInInspector] public int maxBombCount;
    [field: HideInInspector] public int curBombCount;
    [field: HideInInspector] public Vector3 tilePosition;
    [field: HideInInspector] public MonoLink bomberObject;
}
