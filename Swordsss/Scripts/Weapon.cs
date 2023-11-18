using System.Reflection.Metadata;
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
}