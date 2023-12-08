using System;
using Godot;

namespace Swordsss.Scripts;

public partial class PlayerInput : Node2D
{
    public static PlayerInput Instance { get; private set; }

    public event Action<double, Vector2> OnPlayerMovePhysics;
    public event Action<double, Vector2> OnPlayerMove;
    public event Action OnPlayerAttack;
    public event Action OnPlayerAttackPhysics;
    public event Action<Vector2> OnPointerMoved;
    public event Action OnPlayerPause;

    public Vector2 PointerPosition { get; set; }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == NotificationEnterTree)
        {
            if(Instance != null)
                GD.PrintErr("Multiple PlayerInput instances");
            Instance = this;
        }
        if(what == NotificationExitTree)
            Instance = null;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if(Input.IsActionJustPressed(PlayerInputName.Pause))
            OnPlayerPause?.Invoke();
        
        var move = new Vector2();
        if (Input.IsActionPressed(PlayerInputName.MoveUp))
            move.Y -= 1;
        if (Input.IsActionPressed(PlayerInputName.MoveDown))
            move.Y += 1;
        if (Input.IsActionPressed(PlayerInputName.MoveLeft))
            move.X -= 1;
        if (Input.IsActionPressed(PlayerInputName.MoveRight))
            move.X += 1;

        move = move.Normalized();

        OnPlayerMove?.Invoke(delta, move);

        if (Input.IsActionJustPressed(PlayerInputName.Attack))
            OnPlayerAttack?.Invoke();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._Process(delta);

        var move = new Vector2();
        if (Input.IsActionPressed(PlayerInputName.MoveUp))
            move.Y -= 1;
        if (Input.IsActionPressed(PlayerInputName.MoveDown))
            move.Y += 1;
        if (Input.IsActionPressed(PlayerInputName.MoveLeft))
            move.X -= 1;
        if (Input.IsActionPressed(PlayerInputName.MoveRight))
            move.X += 1;

        move = move.Normalized();

        OnPlayerMovePhysics?.Invoke(delta, move);

        if (Input.IsActionJustPressed(PlayerInputName.Attack))
            OnPlayerAttackPhysics?.Invoke();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            PointerPosition = GetGlobalMousePosition();
            OnPointerMoved?.Invoke(PointerPosition);
        }
    }
}


public static class PlayerInputName
{
    public const string MoveLeft = "move_left";
    public const string MoveRight = "move_right";
    public const string MoveUp = "move_up";
    public const string MoveDown = "move_down";

    public const string Attack = "attack";
    
    public const string Pause = "pause";
}