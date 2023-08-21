using Leopotam.Ecs;

public class GetLevelPowerUpSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnGetLevelPowerUp> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var bomber = ref _filter.Get1(i);
            bomber.bombLevel++;
            _filter.GetEntity(i).Del<OnGetLevelPowerUp>();
        }
    }
}
