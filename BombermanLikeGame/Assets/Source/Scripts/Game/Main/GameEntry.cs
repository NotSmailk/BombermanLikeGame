using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;
using Voody.UniLeo;

public class GameEntry : MonoBehaviour
{
    [field: SerializeField] private LevelParametres _parametres;
    [field: SerializeField] private EcsUiEmitter _uiEmitter;
    [field: SerializeField] private SceneData _sceneData;

    private EcsWorld _world = null;
    private EcsSystems _runSystems = null;
    private EcsSystems _initSystems = null;
    private EcsSystems _uiSystems;
    private Grid _grid = null;
    private GameProgress _progress = null;
    private TagCollection _tagCollection = null;
    private PowerUpCollection _powerUpCollection = null;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _progress = new GameProgress();
        _powerUpCollection = new PowerUpCollection();
        _tagCollection = new TagCollection();
        _grid = new Grid(_parametres.Rows, _parametres.Columns);
        _world = new EcsWorld();
        _uiSystems = new EcsSystems(_world);
        _runSystems = new EcsSystems(_world);
        _initSystems = new EcsSystems(_world);
        _uiSystems = new EcsSystems(_world);

        _uiSystems.ConvertScene();
        _runSystems.ConvertScene();
        _initSystems.ConvertScene();

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_runSystems);
#endif

        _uiSystems.
            Add(new StartGameButtonSystem()).
            Add(new RestartGameButtonSystem()).
            Add(new BomberTagPositionSystem()).
            InjectUi(_uiEmitter).
            Inject(_progress).
            Inject(_sceneData);
        _uiSystems.Init();

        _progress.GameInit = GameInit;
        _progress.GameEnd = GameEnd;
        _progress.GameRestart = GameRestart;
    }

    public void GameInit()
    {
        _progress.GamePaused = false;
        AddInjections();
        AddOneFrames();
        AddSystems();

        _runSystems.Init();
        _initSystems.Init();
    }

    public void AddInjections()
    {
        _runSystems.Inject(_parametres);
        _runSystems.Inject(_sceneData);
        _runSystems.Inject(_progress);
        _runSystems.Inject(_tagCollection);
        _runSystems.Inject(_powerUpCollection);
        _runSystems.Inject(_grid);

        _initSystems.Inject(_parametres);
        _initSystems.Inject(_sceneData);
        _initSystems.Inject(_tagCollection);
        _initSystems.Inject(_progress);
        _initSystems.Inject(_grid);
    }

    public void AddSystems()
    {
        _initSystems.
            Add(new TileFactorySpawnSystem()).
            Add(new WallFactorySpawnSystem()).
            Add(new BrickFactorySpawnSystem()).
            Add(new PlayerSpawnSystem()).
            Add(new EnemyFactorySpawnSystem()).
            Add(new BomberTagInitSystem());

        _runSystems.
            Add(new PlayerInputSystem()).
            Add(new EnemyBehaviourSystem()).
            Add(new MoveSystem()).
            Add(new BombFactorySpawnSystem()).
            Add(new TileTriggerSystem()).
            Add(new BombControlSystem()).
            Add(new BomberCooldownSystem()).
            Add(new BombBricksCollisionSystem()).
            Add(new BombBomberCollisionSystem()).
            Add(new BomberTagDestroySystem()).
            Add(new BomberDeathCheckerSystem()).
            Add(new PowerUpTransformSystem()).
            Add(new GetCountPowerUpSystem()).
            Add(new GetLevelPowerUpSystem());
    }

    public void AddOneFrames()
    {

    }

    public void GameEnd()
    {
        _sceneData.restartButton.gameObject.SetActive(true);
        _sceneData.ResetData();
        _tagCollection.ClearAll();
        _powerUpCollection.ClearAll();
    }

    public void GameRestart()
    {
        _runSystems?.Destroy();
        _runSystems = new EcsSystems(_world);
        _initSystems?.Destroy();
        _initSystems = new EcsSystems(_world);

        GameInit();
    }

    private void Update()
    {
        _uiSystems.Run();

        if (_progress.GameStarted)
        {
            if (_progress.GamePaused)
                return;

            _runSystems.Run();
        }
    }

    private void OnDestroy()
    {
        _runSystems?.Destroy();
        _runSystems = null;
        _initSystems?.Destroy();
        _initSystems = null;
        _world?.Destroy();
        _world = null;
    }
}
