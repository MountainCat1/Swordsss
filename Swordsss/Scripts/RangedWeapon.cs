using System;
using Godot;

namespace Swordsss.Scripts;

public partial class RangedWeapon : IWeapon
{
    [Export] public int Damage { get; set; }
    [Export] public float PushAmount { get; set; }
    [Export] public float MissileSpeed { get; set; }
    [Export] public float Range { get; set; }
    
    public bool CanAttack(Node2D attacker, IDamageable damageable)
    {
        if(attacker.Position.DistanceTo(damageable.GlobalPosition) > Range)
            return false;
        
        return true;
    }

    public bool Attack(IDamageable damageable)
    {
        // damageable.Health.DealDamage(Damage);
        //
        // _cooldownTimer.Start();
        // _readyToAttack = false;
        //
        // Attacked?.Invoke();

        // TODO: please work on it its not done xD
        
        return true;
    }

    public event Action Attacked;
    public event Action CooldownEnded;
}