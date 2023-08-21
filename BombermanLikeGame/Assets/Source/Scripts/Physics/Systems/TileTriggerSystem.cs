using Leopotam.Ecs;

public class TileTriggerSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<MoveComponent, BomberComponent, OnTriggerEnterEvent> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var entity = ref _filter.GetEntity(i);
            ref var move = ref _filter.Get1(i);
            ref var bomber = ref _filter.Get2(i);
            ref var trigger = ref _filter.Get3(i);
            ref var tilePos = ref bomber.tilePosition;
            move.gridPosition = trigger.sender.GetComponent<MonoLink>().entity.Get<TileComponent>().gridPosition;
            tilePos = trigger.sender.transform.position;
            entity.Del<OnTriggerEnterEvent>();
        }
    }
}
