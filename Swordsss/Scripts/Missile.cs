using Godot;

namespace Swordsss.Scripts;

public partial class Missile : Area2D
{
    [Export] public Vector2 Direction { get; set; }
    [Export] public float Speed { get; set; }
    [Export] public int Damage { get; set; }

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