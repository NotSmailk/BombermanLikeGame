using Leopotam.Ecs;

public class BomberTagDestroySystem : IEcsRunSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, OnBombCollisionEvent, BomberTagPanelComponent> _filter = null;
    private TagCollection _tags = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var tag = ref _filter.Get3(i);
            _tags.Remove(tag.tagRect);
        }
    }
}
