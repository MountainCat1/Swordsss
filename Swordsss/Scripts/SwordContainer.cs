using System;
using Godot;

namespace Swordsss.Scripts;

public partial class SwordContainer : Node2D
{
    [Export] public float PushAmount { get; set; } = 1000f;
    [Export] public int Damage { get; set; } = 1;
    [Export] public float SwordSpeed { get; set; } = 35;
    [Export] public float SwordPerTime { get; set; } = 1;
    
    public override void _Ready()
    {
        base._Ready();
        PlayerInput.Instance.OnPointerMoved += OnPointerMoved;
        GetNode<Area2D>("Area2D").BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
        SwordSpeed += (int)delta * SwordPerTime;
        base._Process(delta);
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
        Rotation = AngleUtils.LerpAngle(Rotation, angle, SwordSpeed * (float)GetProcessDeltaTime());
    }
}

public static class AngleUtils
{
    public static float LerpAngle(float from, float to, float weight)
    {
        return from + ShortAngleDist(from, to) * weight;
    }

    public static float ShortAngleDist(float from, float to)
    {
        float maxAngle = (float)Math.PI * 2;
        float difference = Mod(to - from, maxAngle);
        return Mod(2 * difference, maxAngle) - difference;
    }

    // Custom modulo function for floating point numbers
    private static float Mod(float a, float b)
    {
        return (a % b + b) % b;
    }
}