using Godot;

namespace Swordsss.Scripts;

public partial class Missile : Area2D
{
    [Export]
    public Vector2 Direction
    {
        get => _direction;
        set => _direction = value.Normalized();
    }

    [Export] public float Speed { get; set; }
    [Export] public int Damage { get; set; }
    [Export] public float PushAmount { get; set; }
    [Export] public float Timeout { get; set; }
    
    private Vector2 _direction;
    
    public override void _Ready()
    {
        base._Ready();

        BodyEntered += body =>
        {
            if (body is Player player)
            {
                OnHitPlayer(player);
            }
        };

        var timeoutTimer = GetNode<Timer>("TimeoutTimer");
        timeoutTimer.WaitTime = Timeout;
        timeoutTimer.Timeout += QueueFree;
        timeoutTimer.Start();
    }

    private void OnHitPlayer(Player player)
    {
        player.Health.DealDamage(Damage);
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
        GlobalPosition += Direction * (float)delta * Speed;
    }
}