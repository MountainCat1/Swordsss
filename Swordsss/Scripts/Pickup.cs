using Godot;

namespace Swordsss.Scripts;

public abstract partial class Pickup : Area2D
{
    public override void _Ready()
    {
        base._Ready();
        
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            OnPickedUp();
        }
    }

    protected virtual void OnPickedUp()
    {
        QueueFree();
        AudioManager.Play(GlobalPosition, Sounds.Pickup);
    }
}