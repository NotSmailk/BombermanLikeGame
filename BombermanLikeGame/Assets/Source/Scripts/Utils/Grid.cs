using Leopotam.Ecs;
using UnityEngine;

public class Grid
{
    public TileMonoLink[,] tiles;

    public Grid(int r, int c)
    {
        tiles = new TileMonoLink[r, c];
    }

    public T CreateItem<T>(T prefab, Transform parent, Vector3 pos) where T : MonoLink
    {
        var tile = Object.Instantiate(prefab);
        tile.transform.SetParent(parent);
        tile.transform.SetPos(pos);

        return tile;
    }

    public void SetTileState(Vector2Int gridPosition, TileState state)
    {
        tiles[gridPosition.x, gridPosition.y].entity.Get<TileComponent>().tileState = state;
    }

    public bool HasNeighboorUp(Vector3 tile, out TileComponent neighboor)
    {
        foreach (var i in tiles)
        {
            if (i.transform.position.z == (tile.z + 1) && i.transform.position.x == tile.x)
            {
                neighboor = i.entity.Get<TileComponent>();
                return true;
            }
        }

        neighboor = default;
        return false;
    }

    public bool HasNeighboorDown(Vector3 tile, out TileComponent neighboor)
    {
        foreach (var i in tiles)
        {
            if (i.transform.position.z == (tile.z - 1) && i.transform.position.x == tile.x)
            {
                neighboor = i.entity.Get<TileComponent>();
                return true;
            }
        }

        neighboor = default;
        return false;
    }

    public bool HasNeighboorLeft(Vector3 tile, out TileComponent neighboor)
    {
        foreach (var i in tiles)
        {
            if (i.transform.position.x == (tile.x - 1) && i.transform.position.z == tile.z)
            {
                neighboor = i.entity.Get<TileComponent>();
                return true;
            }
        }

        neighboor = default;
        return false;
    }

    public bool HasNeighboorRight(Vector3 tile, out TileComponent neighboor)
    {
        foreach (var i in tiles)
        {
            if (i.transform.position.x == (tile.x + 1) && i.transform.position.z == tile.z)
            {
                neighboor = i.entity.Get<TileComponent>();
                return true;
            }
        }

        neighboor = default;
        return false;
    }
}