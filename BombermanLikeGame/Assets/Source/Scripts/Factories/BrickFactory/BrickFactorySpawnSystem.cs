using Leopotam.Ecs;
using UnityEngine;

public class BrickFactorySpawnSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private LevelParametres _parametres = null;
    private SceneData _sceneData = null;
    private Grid _grid;

    public void Init()
    {
        var fatoryEntity = _world.NewEntity();

        ref var factoryComponent = ref fatoryEntity.Get<BrickFactoryComponent>();
        factoryComponent = _sceneData.brickFactory.factoryComponent;

        var cols = _parametres.Columns;
        var rows = _parametres.Rows;

        for (int r = 0; r < rows; r++)
        {
            bool isFirstOrLast = r <= 1 || r >= rows - 2;
            bool isEven = r % 2 == 0;
            int startCol = isFirstOrLast ? 2 : 0;
            int add = isFirstOrLast ? 2 : 0;
            int step = isEven ? 1 : 2;

            for (int c = startCol; c < cols - add; c += step)
            {
                var pos = GridPosition.GetPosition(r, rows, c, cols, 0.5f);
                var brickEntity = _world.NewEntity();
                brickEntity.Get<BombCollisionTag>();
                ref var tag = ref brickEntity.Get<BrickComponent>();

                var brick = _grid.CreateItem(factoryComponent.brickPrefab, factoryComponent.factoryTransform, pos);
                _sceneData.brickFactory.Links.Add(brick);
                _grid.SetTileState(new Vector2Int(r, c), TileState.Brick);
                tag.brick = brick;
                brick.transform.SetParent(factoryComponent.factoryTransform);
                brick.transform.SetPos(pos);
                brick.transformComponent.transform.RotY(Random.Range(-5f, 5f));
                brick.MakeRef(ref brickEntity);
            }
        }

        _sceneData.brickFactory.MakeRef(ref fatoryEntity);
    }
}
