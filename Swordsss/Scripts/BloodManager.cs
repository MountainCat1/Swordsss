using Godot;

namespace Swordsss.Scripts;

public partial class BloodManager : Node
{
    public static BloodManager Instance { get; private set; }
    
    [Export] public PackedScene BloodPrefab { get; set; }
    
    public BloodManager()
    {
        if(Instance != null)
            throw new System.Exception("BloodManager already exists");
        
        Instance = this;
    }
    
    public void PlaceBlood(Vector2 position)
    {
        var blood = BloodPrefab.Instantiate<CpuParticles2D>();
        blood.Emitting = true;
        blood.GlobalPosition = position;
        AddChild(blood);
    }
}