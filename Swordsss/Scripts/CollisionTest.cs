using Godot;
using System;

public partial class CollisionTest : RigidBody2D
{
    public override void _Ready()
    {
        base._Ready();
    }

    private void OnBodyEntered(Node body)
    {
    }
}
