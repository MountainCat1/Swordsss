using Godot;
using System;

public partial class WeaponContainer : Node2D
{
    public override void _Ready()
    {
        base._Ready();
        PlayerInput.Instance.OnPointerMoved += OnPointerMoved;
    }

    private void OnPointerMoved(Vector2 pointerGlobalPosition)
    {
        var direction = (pointerGlobalPosition - GlobalPosition).Normalized();
        var angle = direction.Angle();
        Rotation = angle;
        
    }
}