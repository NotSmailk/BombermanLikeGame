using Leopotam.Ecs;
using UnityEngine;

public class TileFactorySpawnSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private LevelParametres _levelParametres = null;
    private SceneData _sceneData = null;
    private Grid _grid = null;

    public void Init()
    {
        ref var factoryComponent = ref _sceneData.tileFactory.factoryComponent;
        var cols = _levelParametres.Columns;
        var rows = _levelParametres.Rows;

        for (int c = 0; c < cols; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                var pos = GridPosition.GetPosition(r, rows, c, cols, 0.5f);
                var tileEntity = _world.NewEntity();
                ref var comp = ref tileEntity.Get<TileComponent>();

                var tile = _grid.CreateItem(factoryComponent.tilePrefab, factoryComponent.factoryTransform, pos);
                _sceneData.tileFactory.Links.Add(tile);
                tile.MakeRef(ref tileEntity);
                comp.transform = tile.transform;
                comp.gridPosition = new Vector2(r, c);
                _grid.tiles[r, c] = tile;
                _grid.SetTileState(new Vector2Int(r, c), TileState.Empty);
            }
        }
    }
}
