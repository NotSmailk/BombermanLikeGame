using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public BrickFactoryMonoLink brickFactory;
    public WallFactoryMonoLink wallFactory;
    public TileFactoryMonoLink tileFactory;
    public BombFactoryMonoLink bombFactory;
    public EnemyFactoryMonoLink enemyFactory;
    public Camera mainCamera;
    public Canvas canvas;
    public Button restartButton;

    public void ResetData()
    {
        brickFactory.ClearAll();
        wallFactory.ClearAll();
        tileFactory.ClearAll();
        bombFactory.ClearAll();
        enemyFactory.ClearAll();
    }
}
