using Leopotam.Ecs;

public class BombBomberCollisionSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnBombCollisionEvent> _filter = null;
    private LevelParametres _parametres;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var bomber = ref _filter.Get1(i);
            ref var entity = ref _filter.GetEntity(i);

            entity.Get<OnBombCollisionEvent>();
        }
    }
}
