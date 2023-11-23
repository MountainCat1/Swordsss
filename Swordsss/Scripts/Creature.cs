﻿using System;
using Godot;

namespace Swordsss.Scripts;

public partial class Creature : CharacterBody2D, IDamageable
{
    public event Action OnKilled;
    
    
    public IHealth Health { get; private set; }
    
    [Export] public float Speed { get; set; }

    protected Vector2 Momentum { get; set; }
    
    protected AnimatedSprite2D AnimatedSprite2D { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Health = GetNode<Health>("Health");
        AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        ApplyFriction(delta);
    }

    public void Push(Vector2 momentum)
    {
        Momentum += momentum;
    }

    protected void MovementTo(Vector2 target)
    {
        var controlledMove = (target - GlobalPosition).Normalized();
        
        Velocity = controlledMove * Speed + Momentum;
        
        MoveAndSlide();
        
        if (controlledMove.X < 0)
            AnimatedSprite2D.FlipH = true;
        else if(controlledMove.X > 0)
            AnimatedSprite2D.FlipH = false;
    }
    
    protected void ApplyFriction(double delta)
    {
        Momentum = Momentum.MoveToward(Vector2.Zero, Constants.Friction * (float)delta);
    }
    
    protected void Kill()
    {
        OnKilled?.Invoke();
        QueueFree();
    }
}