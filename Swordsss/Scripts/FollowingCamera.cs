using Godot;

namespace Swordsss.Scripts;

public partial class FollowingCamera : Camera2D
{
    [Export] public Node2D Target { get; set; }
    
    public override void _Process(double delta)
    {
        base._Process(delta);

        GlobalPosition = Target.GlobalPosition;
    }
}