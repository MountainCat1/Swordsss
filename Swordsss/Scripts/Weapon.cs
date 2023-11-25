using System;
using Godot;

namespace Swordsss.Scripts;

public interface IWeaponHolder
{
    public IWeapon Weapon { get; }
}

public interface IWeapon
{
    bool CanAttack(Node2D attacker, IDamageable damageable);
    bool Attack(IDamageable damageable);
    event Action Attacked;
    event Action CooldownEnded;
}

public abstract partial class Weapon : Node2D, IWeapon
{
    [Export] public float AttacksPerSecond { get; set; }


    public abstract bool CanAttack(Node2D attacker, IDamageable damageable);

    public abstract bool Attack(IDamageable damageable);

    public abstract event Action Attacked;
    public event Action CooldownEnded;
    
    
    private Timer _cooldownTimer;
    protected bool ReadyToAttack { private set; get; }

    public override void _Ready()
    {
        base._Ready();
        ReadyToAttack = true;
        _cooldownTimer = GetNode<Timer>("CooldownTimer");
        _cooldownTimer.Stop();
        _cooldownTimer.WaitTime = 1f / AttacksPerSecond;
        _cooldownTimer.Timeout += CooldownTimerOnTimeout;
    }
    
    private void CooldownTimerOnTimeout()
    {
        ReadyToAttack = true;
        CooldownEnded?.Invoke();
        _cooldownTimer.Start();
    }
    
    protected void StartWeaponCooldown()
    {
        _cooldownTimer.Start();
        ReadyToAttack = false;
    }
}