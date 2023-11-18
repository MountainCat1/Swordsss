using Godot;

namespace Swordsss.Scripts;

public partial class TouchWeapon : Node2D, IWeapon
{
    [Export] public float Range { get; set; } = 18f;
    [Export] public int Damage { get; set; } = 1;
    [Export] public float AttacksPerSecond { get; set; }
    
    private Timer _cooldownTimer;
    private bool _readyToAttack;

    public override void _Ready()
    {
        base._Ready();
        _readyToAttack = true;
        _cooldownTimer = GetNode<Timer>("CooldownTimer");
        _cooldownTimer.Stop();
        _cooldownTimer.WaitTime = 1f / AttacksPerSecond;
        _cooldownTimer.Timeout += CooldownTimerOnTimeout;
    }

    private void CooldownTimerOnTimeout()
    {
        _readyToAttack = true;
        _cooldownTimer.Start();
    }

    public bool CanAttack(Node2D attacker, IDamageable damageable)
    {
        if (!_readyToAttack)
            return false;
        
        if (attacker.GlobalPosition.DistanceTo(damageable.GlobalPosition) > Range)
            return false;
         
        return true;
    }

    public bool Attack(IDamageable damageable)
    {
        damageable.Health.DealDamage(Damage);

        _cooldownTimer.Start();
        _readyToAttack = false;
        
        return true;
    }
}