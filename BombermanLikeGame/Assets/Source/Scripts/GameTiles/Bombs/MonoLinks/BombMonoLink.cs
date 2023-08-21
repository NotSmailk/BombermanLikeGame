using Leopotam.Ecs;

public class BombMonoLink : MonoLink
{
    protected EcsEntity _sender = default;

    public void SetSender(ref EcsEntity sender)
    {
        _sender = sender;
    }

    public override void DestroyLink()
    {
        if (_sender.IsAlive())
            _sender.Get<OnBombDestroyEvent>();

        if (entity.IsAlive())
            entity.Destroy();

        Destroy(gameObject);
    }
}
