using Leopotam.Ecs;
using UnityEngine;

public class PlayerSpawnSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private LevelParametres _levelParametres;

    public void Init()
    {
        var player = Object.Instantiate(_levelParametres.PlayerPrefab);
        var entity = _world.NewEntity();
        ref var tag = ref entity.Get<PlayerTagComponent>();
        ref var move = ref entity.Get<MoveComponent>();
        ref var bomber = ref entity.Get<BomberComponent>();
        ref var collision = ref entity.Get<BombCollisionTag>();
        bomber.bombLevel = _levelParametres.InitBombLevel;
        bomber.maxBombCount = _levelParametres.InitBombCount;
        bomber.curBombCount = _levelParametres.InitBombCount;

        var cols = _levelParametres.Columns;
        var rows = _levelParametres.Rows;
        var pos = GridPosition.GetPosition(0, rows, 0, cols, 0.5f);
        move = player.moveParametres;
        move.transform.SetPos(pos);
        bomber.bomberObject = player;

        player.MakeRef(ref entity);
    }
}
