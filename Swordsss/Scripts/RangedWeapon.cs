using System;
using Godot;
using Microsoft.Win32.SafeHandles;

namespace Swordsss.Scripts;

public partial class RangedWeapon : Weapon
{
    [Export] public PackedScene MissilePrefab { get; set; }

    [Export] public int Damage { get; set; }
    [Export] public float PushAmount { get; set; }
    [Export] public float MissileSpeed { get; set; }
    [Export] public float Range { get; set; }

    public override bool CanAttack(Node2D attacker, IDamageable damageable)
    {
        if (!ReadyToAttack)
            return false;
        
        if (attacker.Position.DistanceTo(damageable.GlobalPosition) > Range)
            return false;

        return true;
    }

    public override bool Attack(IDamageable damageable)
    {
        var direction = damageable.GlobalPosition - GlobalPosition;

        var missile = MissilePrefab.Instantiate<Missile>();
        
        MissileContainer.Instance.AddChild(missile);
        
        missile.GlobalPosition = GlobalPosition;
        missile.Direction = direction.Normalized();
        missile.Speed = MissileSpeed;
        missile.Damage = Damage;
        missile.PushAmount = PushAmount;

        Attacked?.Invoke();

        StartWeaponCooldown();
        
        return true;
    }

    public override event Action Attacked;
}