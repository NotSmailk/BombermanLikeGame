using Leopotam.Ecs;

public class BomberDeathCheckerSystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnBombCollisionEvent> _filter = null;
    private GameProgress _progress = null;
    private SceneData _data;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var bomber = ref _filter.Get1(i);

            if (_filter.GetEntity(i).IsAlive() && _filter.GetEntity(i).Has<PlayerTagComponent>())
            {
                _progress.GamePaused = true;
                _progress.GameEnd.Invoke();
                bomber.bomberObject.DestroyLink();
            }

            if (_filter.GetEntity(i).IsAlive() && _filter.GetEntity(i).Has<EnemyComponent>())
            {
                _data.enemyFactory.Remove(bomber.bomberObject);
            }
        }
    }
}
