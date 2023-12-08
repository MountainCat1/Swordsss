using Godot;

namespace Swordsss.Scripts;

public partial class SwordContainer : Node2D
{
    [Export] public float PushAmount { get; set; } = 1000f;
    [Export] public int Damage { get; set; } = 1;
    
    public override void _Ready()
    {
        base._Ready();
        PlayerInput.Instance.OnPointerMoved += OnPointerMoved;
        GetNode<Area2D>("Area2D").BodyEntered += OnBodyEntered;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        
        if(what == NotificationPredelete)
            PlayerInput.Instance.OnPointerMoved -= OnPointerMoved;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is IDamageable damageable)
        {
            AudioManager.Play(GlobalPosition, Sounds.HitSound);
            damageable.Health.DealDamage(Damage);
        }
        if(body is Creature creature)
            creature.Push(GlobalPosition.DirectionTo(body.GlobalPosition) * PushAmount);
    }
    
    private void OnPointerMoved(Vector2 pointerGlobalPosition)
    {
        if(GetTree().Paused)
            return;
        
        var direction = (pointerGlobalPosition - GlobalPosition).Normalized();
        var angle = direction.Angle();
        Rotation = angle;
    }
}