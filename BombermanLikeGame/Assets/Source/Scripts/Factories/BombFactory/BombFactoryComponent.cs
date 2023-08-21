using System;
using UnityEngine;

[Serializable]
public struct BombFactoryComponent
{
    public BombMonoLink bombPrefab;
    public ParticleSystem explosionParticles;
    public Transform factoryTransform;
}
