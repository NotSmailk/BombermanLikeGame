using Leopotam.Ecs;

public class PowerUpTransformSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<PowerUpTag, TransformComponent> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var transform = ref _filter.Get2(i);
            transform.transform.Rotate(transform.transform.up, 0.1f);
        }
    }
}
