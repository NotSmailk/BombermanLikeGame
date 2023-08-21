using UnityEngine.Events;

public class GameProgress
{
    public bool GamePaused = false;
    public bool GameStarted = false;
    public UnityAction GameInit;
    public UnityAction GameEnd;
    public UnityAction GameRestart;
}
