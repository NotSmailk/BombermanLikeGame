using Leopotam.Ecs;
using UnityEngine;

public abstract class MonoLink : MonoBehaviour
{
    public EcsEntity entity;

    public virtual void MakeRef(ref EcsEntity entity)
    {
        this.entity = entity;
    }

    public virtual void DestroyLink()
    {
        if (entity.IsAlive())
            entity.Destroy();

        Destroy(gameObject);
    }

    public virtual void DestroyLink(float t)
    {
        if (entity.IsAlive())
            entity.Destroy();

        Destroy(gameObject, t);
    }
}
