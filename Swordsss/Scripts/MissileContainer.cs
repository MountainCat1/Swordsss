using Godot;

namespace Swordsss.Scripts;

public partial class MissileContainer : Node
{
    public static MissileContainer Instance { get; private set; }
    
    public override void _Notification(int what)
    {
        base._Notification(what);
        if(what == NotificationEnterTree)
            Instance = this;
        if(what == NotificationExitTree)
            Instance = null;
    }

}