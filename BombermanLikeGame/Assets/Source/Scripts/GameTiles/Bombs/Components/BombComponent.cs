using System;

[Serializable]
public struct BombComponent
{
    public int bombLevel;
    public float bombTime;
    public BombMonoLink bomb;
}
