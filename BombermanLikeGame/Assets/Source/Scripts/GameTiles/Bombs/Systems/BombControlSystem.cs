using Leopotam.Ecs;
using UnityEngine;

public class BombControlSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BombComponent> _filter = null;
    private SceneData _sceneData = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var bomb = ref _filter.Get1(i);
            ref var time = ref bomb.bombTime;

            if (time <= 0)
            {
                var explosion = Object.Instantiate(_sceneData.bombFactory.factoryComponent.explosionParticles);
                explosion.transform.position = bomb.bomb.transform.position;

                var collision = Physics.SphereCastAll(bomb.bomb.transform.position, 0.2f, bomb.bomb.transform.up);
                foreach (var coll in collision)
                {
                    if (coll.transform.TryGetComponent(out MonoLink link) && link.entity.IsAlive())
                    {
                        if (link.entity.Has<BombCollisionTag>())
                            link.entity.Get<OnBombCollisionEvent>();
                    }
                }

                Object.Destroy(explosion.gameObject, 0.3f);
                _sceneData.bombFactory.Remove(bomb.bomb);

                continue;
            }

            time -= Time.deltaTime;
        }
    }
}
