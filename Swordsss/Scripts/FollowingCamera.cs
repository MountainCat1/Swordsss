using Godot;

namespace Swordsss.Scripts;

public partial class FollowingCamera : Camera2D
{
    [Export] public Node2D Target { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        Target.TreeExited += () => Target = null;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (Target != null)
            GlobalPosition = Target.GlobalPosition;
    }
}