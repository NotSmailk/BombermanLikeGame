using Leopotam.Ecs;

public class GetCountPowerUpSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnGetCountPowerUp> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var bomber = ref _filter.Get1(i);
            bomber.maxBombCount++;
            bomber.curBombCount++;
            _filter.GetEntity(i).Del<OnGetCountPowerUp>();
        }
    }
}
