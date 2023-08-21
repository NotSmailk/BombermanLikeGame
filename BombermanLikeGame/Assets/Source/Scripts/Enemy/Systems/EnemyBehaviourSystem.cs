using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<EnemyComponent, MoveComponent, BomberComponent> _filter = null;
    private float _rayLength = 0.5f;
    private LayerMask _mask = 1 << 7;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var move = ref _filter.Get2(i);
            ref var bomber = ref _filter.Get3(i);

            move.direction = GetDirection(move);
        }
    }

    private Vector3 GetDirection(MoveComponent move)
    {
        Vector3 dir = move.direction;
        var pos = move.transform.position;
        pos.y += 0.25f;
        var possibleDirs = new List<Vector3>();

        if (CheckDirection(pos, dir))
            return dir;

        CheckDirection(pos, Vector3.right, possibleDirs);
        CheckDirection(pos, Vector3.left, possibleDirs);
        CheckDirection(pos, Vector3.forward, possibleDirs);
        CheckDirection(pos, Vector3.back, possibleDirs);

        return possibleDirs.Random();
    }

    private bool CheckDirection(Vector3 pos, Vector3 dir)
    {
        var ray = new Ray(pos, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, _mask))
        {
            if (hit.transform.TryGetComponent(out MonoLink link) && link.entity.IsAlive())
            {
                if (link.entity.Has<BrickComponent>() || link.entity.Has<WallComponent>())
                    return false;
            }
        }

        return true;
    }

    private bool CheckDirection(Vector3 pos, Vector3 dir, List<Vector3> dirs)
    {
        var ray = new Ray(pos, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, _mask))
        {
            if (hit.transform.TryGetComponent(out MonoLink link) && link.entity.IsAlive())
            {
                if (link.entity.Has<BrickComponent>() || link.entity.Has<WallComponent>())
                    return false;
            }
        }

        dirs.Add(dir);
        return true;
    }
}
