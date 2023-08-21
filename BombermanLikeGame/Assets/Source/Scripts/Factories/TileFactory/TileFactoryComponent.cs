using System;
using UnityEngine;

[Serializable]
public struct TileFactoryComponent
{
    public TileMonoLink tilePrefab;
    public BombMonoLink highlightedTilePrefab;
    public Transform factoryTransform;
}
