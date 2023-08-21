using Leopotam.Ecs;
using UnityEngine;

public class BomberTagPositionSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, BomberTagPanelComponent, MoveComponent> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var panel = ref _filter.Get2(i);
            ref var movable = ref _filter.Get3(i);

            panel.tagRect.position = RectTransformUtility.WorldToScreenPoint(_sceneData.mainCamera, movable.transform.position);
        }
    }
}
