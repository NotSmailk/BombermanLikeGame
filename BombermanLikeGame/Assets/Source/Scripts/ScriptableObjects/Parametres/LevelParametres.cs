using UnityEngine;

[CreateAssetMenu(menuName = "Parametres/Level Parametres", fileName = "New Level Parametres")]
public class LevelParametres : ScriptableObject
{
    [field: SerializeField] private int _columns;
    [field: SerializeField] private int _rows;
    [field: SerializeField] private float _bombTime = 3f;
    [field: SerializeField] private int _initBombCount = 1;
    [field: SerializeField] private int _initBombLevel = 1;
    [field: SerializeField] private PlayerMonoLink _playerPrefab;
    [field: SerializeField] private BombCountPowerUpMonoLink _countPowerUp;
    [field: SerializeField] private BombLevelPowerUpMonoLink _levelPowerUp;
    [field: SerializeField] private BomberTagPanel _tagPrefab;

    public int Columns => _columns;
    public int Rows => _rows;
    public float BombTime => _bombTime;
    public int InitBombCount => _initBombCount;
    public int InitBombLevel => _initBombLevel;
    public PlayerMonoLink PlayerPrefab => _playerPrefab;
    public BombCountPowerUpMonoLink CountPowerUp => _countPowerUp;
    public BombLevelPowerUpMonoLink LevelPowerUp => _levelPowerUp;
    public BomberTagPanel TagPrefab => _tagPrefab;
}
