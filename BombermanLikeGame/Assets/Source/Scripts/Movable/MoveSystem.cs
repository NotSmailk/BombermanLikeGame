using Leopotam.Ecs;

public class MoveSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<MoveComponent> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var move = ref _filter.Get1(i);
            ref var rigidbody = ref move.rigidbody;

            var dir = move.direction;
            var speed = move.speed;
            var velocity = dir * speed;

            rigidbody.velocity = velocity;
        }
    }
}
