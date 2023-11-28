using Godot;

namespace Swordsss.Scripts;

public partial class BloodManager : Node
{
    public static BloodManager Instance { get; private set; }

    [Export] public PackedScene BloodPrefab { get; set; }
    
    public override void _Notification(int what)
    {
        base._Notification(what);
        if(what == NotificationEnterTree)
            Instance = this;
        if(what == NotificationExitTree)
            Instance = null;
    }

    public void PlaceBlood(Vector2 position)
    {
        var blood = BloodPrefab.Instantiate<CpuParticles2D>();
        blood.Emitting = true;
        blood.GlobalPosition = position;
        AddChild(blood);
    }
}