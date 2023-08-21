using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<PlayerTagComponent, MoveComponent, BomberComponent> _filter = null;

    private float _moveX = 0;
    private float _moveZ = 0;
    private bool _placeBomb = false;

    public void Run()
    {
        SetDirection();
        GetBombButton();

        foreach(var i in _filter)
        {
            ref var move = ref _filter.Get2(i);
            ref var bomber = ref _filter.Get3(i);
            ref var direction = ref move.direction;

            direction.x = _moveX;
            direction.z = _moveZ;

            if (_placeBomb)
                _filter.GetEntity(i).Get<OnPlaceBombEvent>();
        }
    }

    private void SetDirection()
    {
        _moveZ = Input.GetAxis("Vertical");
        _moveX = Input.GetAxis("Horizontal");
    }

    private void GetBombButton()
    {
        _placeBomb = Input.GetKeyDown(KeyCode.Space);
    }
}
