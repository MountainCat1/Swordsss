using Godot;
using System;
using Swordsss.Scripts;

public partial class WeaponContainer : Node2D
{
    public override void _Ready()
    {
        base._Ready();
        PlayerInput.Instance.OnPointerMoved += OnPointerMoved;
        GetNode<Area2D>("Area2D").BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is IDamageable damageable)
        {
            // TODO: this is just a draft please change me later
            damageable.Health.DealDamage(1);
        }
    }
    
    private void OnPointerMoved(Vector2 pointerGlobalPosition)
    {
        var direction = (pointerGlobalPosition - GlobalPosition).Normalized();
        var angle = direction.Angle();
        Rotation = angle;
    }
}