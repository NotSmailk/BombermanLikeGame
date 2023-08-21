using Leopotam.Ecs;
using UnityEngine;

public class WallFactorySpawnSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private LevelParametres _parametres = null;
    private SceneData _sceneData = null;
    private Grid _grid;

    public void Init()
    {
        var entity = _world.NewEntity();
        ref var factoryComponent = ref entity.Get<WallFactoryComponent>();
        factoryComponent = _sceneData.wallFactory.factoryComponent;
        var cols = _parametres.Columns;
        var rows = _parametres.Rows;

        // Inner walls
        for (int c = 1; c < cols; c += 2)
        {
            for (int r = 1; r < rows; r += 2)
            {
                var pos = GridPosition.GetPosition(r, rows, c, cols, 0.5f);
                CreateEntity(factoryComponent, pos);
                _grid.SetTileState(new Vector2Int(r, c), TileState.Wall);
            }
        }

        // Uppers walls
        for (int c = -1; c <= cols; c++)
        {
            var pos = GridPosition.GetPosition(0, -rows, c, cols, 0.5f);
            CreateEntity(factoryComponent, pos);
        }

        // Bottom walls
        for (int c = -1; c <= cols; c++)
        {
            var pos = GridPosition.GetPosition(-1, rows, c, cols, 0.5f);
            CreateEntity(factoryComponent, pos);
        }

        // Right walls
        for (int r = 0; r < rows; r++)
        {
            var pos = GridPosition.GetPosition(r, rows, 0, -cols, 0.5f);
            CreateEntity(factoryComponent, pos);
        }

        // Left walls
        for (int r = 0; r < rows; r++)
        {
            var pos = GridPosition.GetPosition(r, rows, -1, cols, 0.5f);
            CreateEntity(factoryComponent, pos);
        }
    }

    private void CreateEntity(WallFactoryComponent factoryComponent, Vector3 pos)
    {
        var wallEntity = _world.NewEntity();
        ref var tag = ref wallEntity.Get<WallComponent>();

        var wall = _grid.CreateItem(factoryComponent.wallPrefab, factoryComponent.factoryTransform, pos);
        _sceneData.wallFactory.Links.Add(wall);
        wall.transform.SetParent(factoryComponent.factoryTransform);
        wall.transform.SetPos(pos);
        wall.MakeRef(ref wallEntity);
    }
}
