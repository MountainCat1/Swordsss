using Godot;

namespace Swordsss.Scripts;

public partial class MissileContainer : Node
{
    public static MissileContainer Instance { get; private set; }
    
    public MissileContainer()
    {
        if(Instance != null)
            throw new System.Exception("MissileContainer already exists");
        
        Instance = this;
    }
}