using System;
using Godot;

namespace Swordsss.Scripts;

public interface IDamageable
{
    public IHealth Health { get; }
    public Vector2 GlobalPosition { get; set; }
}

public interface IHealth
{
    public float Amount { get; set; }
    public float Max { get; set; }
    void DealDamage(int i);
    public event Action Changed;
    public event Action Depleted;
}

[GlobalClass]
public partial class Health : Node, IHealth
{
    [Export] public float Amount { get; set; }
    [Export] public float Max { get; set; }
    
    public void DealDamage(int i)
    {
        Amount -= i;
        Changed?.Invoke();
        
        if(Amount <= 0)
            Depleted?.Invoke();
    }

    public event Action Changed;
    public event Action Depleted;
}