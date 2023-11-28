using Godot;
using System;
using Swordsss.Scripts;

public partial class Player : Creature
{
    public static Player Instance { get; private set; }
    
    public override void _Ready()
    {
        base._Ready();

        var playerInput = PlayerInput.Instance;
        
        playerInput.OnPlayerMovePhysics += OnPlayerMove;
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if(what == NotificationPredelete)
            PlayerInput.Instance.OnPlayerMovePhysics -= OnPlayerMove;
        if(what == NotificationEnterTree)
            Instance = this;
        if(what == NotificationExitTree)
            Instance = null;
    }


    private void OnPlayerMove(double delta, Vector2 direction)
    {
        Velocity = direction * Speed;
        
        MoveAndSlide();
        
        if (direction.X < 0)
            AnimatedSprite2D.FlipH = true;
        else if(direction.X > 0)
            AnimatedSprite2D.FlipH = false;
    }
}