using System;
using Godot;

namespace Swordsss.Scripts;

public partial class TouchWeapon : Weapon
{
    #region Events
    
    public override event Action Attacked;
    
    #endregion
    
    [Export] public float Range { get; set; } = 18f;
    [Export] public int Damage { get; set; } = 1;
    

    public override bool CanAttack(Node2D attacker, IDamageable damageable)
    {
        if (!ReadyToAttack)
            return false;
        
        if (attacker.GlobalPosition.DistanceTo(damageable.GlobalPosition) > Range)
            return false;
         
        return true;
    }

    public override bool Attack(IDamageable damageable)
    {
        damageable.Health.DealDamage(Damage);

        StartWeaponCooldown();
        
        Attacked?.Invoke();
        
        AudioManager.Play(GlobalPosition, Sounds.ChompSound);

        return true;
    }
}