using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerEnterMonoLink : MonoLink
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PhysicalMonoLink link))
        {
            ref var trigger = ref link.entity.Get<OnTriggerEnterEvent>();
            trigger.sender = gameObject;
            trigger.collider = other;
        }
    }
}
