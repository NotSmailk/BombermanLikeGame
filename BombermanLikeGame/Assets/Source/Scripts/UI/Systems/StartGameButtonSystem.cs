using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;

public class StartGameButtonSystem : IEcsRunSystem
{
    private const string widget = "OnStartGame";

    private EcsFilter<EcsUiClickEvent> _filter = null;
    private GameProgress _progress;

    public void Run()
    {
        foreach (var i in _filter)
        {
            var click = _filter.Get1(i);
            if (click.WidgetName.Equals(widget))
            {
                _progress.GameStarted = true;
                click.Sender.SetActive(false);
                _progress.GameInit.Invoke();
            }
        }
    }
}