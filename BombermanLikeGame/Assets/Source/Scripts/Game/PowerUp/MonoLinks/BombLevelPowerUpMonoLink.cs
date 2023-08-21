using Leopotam.Ecs;
using UnityEngine;

public class BombLevelPowerUpMonoLink : MonoLink
{
    public PowerUpCollection collection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PhysicalMonoLink link))
        {
            if (link.entity.Has<BomberComponent>())
            {
                link.entity.Get<OnGetLevelPowerUp>();
                collection.Remove(this);
            }
        }
    }
}
