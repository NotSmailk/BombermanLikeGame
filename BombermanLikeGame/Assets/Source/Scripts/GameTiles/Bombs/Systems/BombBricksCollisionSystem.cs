using Leopotam.Ecs;
using UnityEngine;

public class BombBricksCollisionSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BrickComponent, OnBombCollisionEvent> _filter = null;
    private LevelParametres _parametres;
    private SceneData _sceneData;
    private PowerUpCollection _powerUpCollection;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var brick = ref _filter.Get1(i);
            ref var entity = ref _filter.GetEntity(i);

            CreatePowerUp(brick.brick.transform.position);
            _sceneData.brickFactory.Remove(brick.brick);
        }
    }

    public void CreatePowerUp(Vector3 pos)
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                CreatePowerUp(pos, _parametres.CountPowerUp).collection = _powerUpCollection;
                break;
            case 1:
                CreatePowerUp(pos, _parametres.LevelPowerUp).collection = _powerUpCollection;
                break;
        }
    }

    public T CreatePowerUp<T>(Vector3 pos, T prefab) where T : MonoLink
    {
        var entity = _world.NewEntity();
        entity.Get<PowerUpTag>();
        ref var transform = ref entity.Get<TransformComponent>();
        var powerUp = Object.Instantiate(prefab);
        _powerUpCollection.links.Add(powerUp);
        powerUp.transform.MoveXZ(pos);
        transform.transform = powerUp.transform;
        powerUp.MakeRef(ref entity);

        return powerUp;
    }
}
