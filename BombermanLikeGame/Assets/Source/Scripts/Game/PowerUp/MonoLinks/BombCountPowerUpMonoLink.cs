using Leopotam.Ecs;
using UnityEngine;

public class BombCountPowerUpMonoLink : MonoLink
{
    public PowerUpCollection collection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PhysicalMonoLink link))
        {
            if (link.entity.Has<BomberComponent>())
            {
                link.entity.Get<OnGetCountPowerUp>();
                collection.Remove(this);
            }
        }
    }
}
