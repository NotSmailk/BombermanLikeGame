using Leopotam.Ecs;
using UnityEngine;

public class EnemyFactorySpawnSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private SceneData _data = null;
    private LevelParametres _parametres = null;

    public void Init()
    {
        ref var factory = ref _data.enemyFactory.factoryComponent;

        CreateEnemy(0, _parametres.Columns - 1);
        CreateEnemy(_parametres.Rows - 1, _parametres.Columns - 1);
        CreateEnemy(_parametres.Rows - 1, 0);
    }

    public void CreateEnemy(int r, int c)
    {
        var entity = _world.NewEntity();
        ref var enemy = ref entity.Get<EnemyComponent>();
        enemy.enemyState = EnemyState.Follow;
        ref var move = ref entity.Get<MoveComponent>();
        ref var bomber = ref entity.Get<BomberComponent>();
        bomber.bombLevel = _parametres.InitBombLevel;
        bomber.maxBombCount = bomber.curBombCount = _parametres.InitBombCount;
        ref var collision = ref entity.Get<BombCollisionTag>();
        var pos = GridPosition.GetPosition(r, _parametres.Rows, c, _parametres.Columns, 0.5f);

        var link = Object.Instantiate(_data.enemyFactory.factoryComponent.enemyPrefab);
        _data.enemyFactory.Links.Add(link);
        link.transform.SetParent(_data.enemyFactory.factoryComponent.transform);
        link.transform.SetPos(pos);

        move = link.moveComponent;
        move.direction = Vector3.forward;
        bomber.bomberObject = link;

        link.MakeRef(ref entity);
    }
}