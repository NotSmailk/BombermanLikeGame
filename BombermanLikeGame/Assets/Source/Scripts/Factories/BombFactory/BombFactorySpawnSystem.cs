using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

public class BombFactorySpawnSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnPlaceBombEvent> _filter = null;
    private SceneData _data = null;
    private LevelParametres _parametres;
    private Grid _grid;

    public void Run()
    {
        foreach (var i in _filter)
        {
            var tileFactory = _data.tileFactory;
            ref var entity = ref _filter.GetEntity(i);
            ref var bomber = ref _filter.Get1(i);
            ref var curBombs = ref bomber.curBombCount;
            var factory = _data.bombFactory.factoryComponent;
            entity.Del<OnPlaceBombEvent>();

            if (curBombs > 0)
            {
                curBombs--;
                var bomb = Object.Instantiate(factory.bombPrefab);
                _data.bombFactory.Links.Add(bomb);
                bomb.SetSender(ref entity);
                bomb.transform.SetParent(factory.factoryTransform);
                bomb.transform.SetPos(bomber.tilePosition);

                var bombEnt = _world.NewEntity();

                ref var bombComp = ref bombEnt.Get<BombComponent>();
                bombComp.bombLevel = bomber.bombLevel;
                bombComp.bombTime = _parametres.BombTime;
                bombComp.bomb = bomb;

                CreateHighlights(tileFactory, bomber);

                bomb.MakeRef(ref bombEnt);
            }
        }
    }

    private void CreateHighlights(TileFactoryMonoLink tileFactory, BomberComponent bomber)
    {
        Vector3 pos = bomber.tilePosition;
        for (int i = 0; i < bomber.bombLevel; i++)
        {
            if (_grid.HasNeighboorUp(pos, out TileComponent neighboor1))
            {
                if (IsBrick(neighboor1.transform.position))
                {
                    i = bomber.bombLevel;
                    CreateHighlight(tileFactory, bomber, neighboor1);
                    continue;
                }

                if (IsWall(neighboor1.transform.position))
                {
                    i = bomber.bombLevel;
                    continue;
                }

                pos = neighboor1.transform.position;
                CreateHighlight(tileFactory, bomber, neighboor1);
            }
        }

        pos = bomber.tilePosition;
        for (int i = 0; i < bomber.bombLevel; i++)
        {
            if (_grid.HasNeighboorLeft(pos, out TileComponent neighboor2))
            {
                if (IsBrick(neighboor2.transform.position))
                    i = bomber.bombLevel;

                if (IsWall(neighboor2.transform.position))
                {
                    i = bomber.bombLevel;
                    continue;
                }

                pos = neighboor2.transform.position;
                CreateHighlight(tileFactory, bomber, neighboor2);
            }
        }

        pos = bomber.tilePosition;
        for (int i = 0; i < bomber.bombLevel; i++)
        {
            if (_grid.HasNeighboorDown(pos, out TileComponent neighboor3))
            {
                if (IsBrick(neighboor3.transform.position))
                {
                    i = bomber.bombLevel;
                    CreateHighlight(tileFactory, bomber, neighboor3);
                    continue;
                }

                if (IsWall(neighboor3.transform.position))
                {
                    i = bomber.bombLevel;
                    continue;
                }

                pos = neighboor3.transform.position;
                CreateHighlight(tileFactory, bomber, neighboor3);
            }
        }

        pos = bomber.tilePosition;
        for (int i = 0; i < bomber.bombLevel; i++)
        {
            if (_grid.HasNeighboorRight(pos, out TileComponent neighboor4))
            {
                if (IsBrick(neighboor4.transform.position))
                {
                    i = bomber.bombLevel;
                    CreateHighlight(tileFactory, bomber, neighboor4);
                    continue;
                }

                if (IsWall(neighboor4.transform.position))
                {
                    i = bomber.bombLevel;
                    continue;
                }

                pos = neighboor4.transform.position;
                CreateHighlight(tileFactory, bomber, neighboor4);
            }
        }
    }

    private void CreateHighlight(TileFactoryMonoLink tileFactory, BomberComponent bomber, TileComponent neighboor)
    {
        var highlight = Object.Instantiate(tileFactory.factoryComponent.highlightedTilePrefab);
        _data.bombFactory.Links.Add(highlight);
        highlight.transform.SetParent(tileFactory.factoryComponent.factoryTransform);
        highlight.transform.SetPos(neighboor.transform.position);
        var bombEnt = _world.NewEntity();
        ref var bombComp = ref bombEnt.Get<BombComponent>();
        bombComp.bombLevel = bomber.bombLevel;
        bombComp.bombTime = _parametres.BombTime;
        bombComp.bomb = highlight;
        highlight.MakeRef(ref bombEnt);
    }

    private bool IsWall(Vector3 position)
    {
        var collision = Physics.OverlapSphere(position, 0.1f, 1 << 7);
        foreach (var coll in collision)
        {
            if (coll.transform.TryGetComponent(out MonoLink link) && link.entity.IsAlive())
            {
                if (link.entity.Has<WallComponent>()) 
                    return true;
            }
        }

        return false;
    }

    private bool IsBrick(Vector3 position)
    {
        var collision = Physics.OverlapSphere(position, 0.1f, 1 << 7);
        foreach (var coll in collision)
        {
            if (coll.transform.TryGetComponent(out MonoLink link) && link.entity.IsAlive())
            {
                if (link.entity.Has<BrickComponent>()) 
                    return true;
            }
        }

        return false;
    }
}
