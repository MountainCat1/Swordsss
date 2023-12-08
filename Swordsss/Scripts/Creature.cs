using System;
using Godot;

namespace Swordsss.Scripts;

public partial class Creature : CharacterBody2D, IDamageable
{
    #region Events
    public event Action OnKilled;

    #endregion
    
    [Export] public float Speed { get; set; }
    
    public IHealth Health { get; private set; }
    protected Vector2 Momentum { get; set; }
    
    protected AnimatedSprite2D AnimatedSprite2D { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        
        Health = GetNode<Health>("Health");
        Health.Depleted += Kill;
        
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

    protected void ApplyMomentum()
    {
        Velocity = Momentum;
    }

    protected void SteerMovement(Vector2 target)
    {
        var controlledMove = (target - GlobalPosition).Normalized();
        
        if (controlledMove.X < 0)
            AnimatedSprite2D.FlipH = true;
        else if(controlledMove.X > 0)
            AnimatedSprite2D.FlipH = false;
        
        Velocity += controlledMove * Speed;
    }
    
    protected void ApplyFriction(double delta)
    {
        Momentum = Momentum.MoveToward(Vector2.Zero, Constants.Friction * (float)delta);
    }
    
    protected void Kill()
    {
        BloodManager.Instance.PlaceBlood(GlobalPosition);
        OnKilled?.Invoke();
        AudioManager.Play(GlobalPosition, Sounds.EnemyDiedSound);
        QueueFree();
    }
}