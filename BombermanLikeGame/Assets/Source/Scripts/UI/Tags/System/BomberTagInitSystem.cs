using Leopotam.Ecs;
using UnityEngine;

public class BomberTagInitSystem : IEcsInitSystem
{
    private EcsWorld _world = null;
    private EcsFilter<BomberComponent, PlayerTagComponent> _playerFilter = null;
    private EcsFilter<BomberComponent, EnemyComponent> _cpuFilter = null;
    private LevelParametres _parametres;
    private SceneData _data;
    private TagCollection _tagCollection;

    public void Init()
    {
        for (int i = 0; i < _playerFilter.GetEntitiesCount(); i++)
        {
            ref var entity = ref _playerFilter.GetEntity(i);
            ref var tagPanel = ref entity.Get<BomberTagPanelComponent>();
            var tag = Object.Instantiate(_parametres.TagPrefab, _data.canvas.transform);
            _tagCollection.panels.Add(tag.tagRect);
            tagPanel.tagText = tag.tagText;
            tagPanel.tagRect = tag.tagRect;
            tagPanel.tagText.text = $"{GameConstants.KeyWords.PLAYER_TAG}{i + 1}";
        }

        for (int i = 0; i < _cpuFilter.GetEntitiesCount(); i++)
        {
            ref var entity = ref _cpuFilter.GetEntity(i);
            ref var tagPanel = ref entity.Get<BomberTagPanelComponent>();
            var tag = Object.Instantiate(_parametres.TagPrefab, _data.canvas.transform);
            _tagCollection.panels.Add(tag.tagRect);
            tagPanel.tagText = tag.tagText;
            tagPanel.tagRect = tag.tagRect;
            tagPanel.tagText.text = $"{GameConstants.KeyWords.CPU_TAG}{i + 1}";
        }
    }
}
