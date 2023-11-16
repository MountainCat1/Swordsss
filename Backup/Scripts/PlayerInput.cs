using Godot;
using System;

public partial class PlayerInput : Node
{
    public static PlayerInput Instance { get; private set; }
    
    [Signal] public delegate void PlayerMovedEventHandler(float x, float y, double delta);
    [Signal] public delegate void PlayerAttackedEventHandler();

    public PlayerInput()
    {
        if(Instance == null)
            Instance = this;
        else
            throw new Exception("Only one instance of PlayerInput is allowed");
    }
    
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if(Input.IsActionJustPressed(PlayerActions.Attack))
            EmitSignal(nameof(PlayerAttacked));
        

        float moveX = 0;
        float moveY = 0;
        if (Input.IsActionPressed(PlayerActions.MoveDown))
            moveY += 1;
        if (Input.IsActionPressed(PlayerActions.MoveUp))
            moveY -= 1;
        if (Input.IsActionPressed(PlayerActions.MoveLeft))
            moveX -= 1;
        if (Input.IsActionPressed(PlayerActions.MoveRight))
            moveX += 1;
        
        if(moveX != 0 || moveY != 0)
            EmitSignal(nameof(PlayerMoved), moveX, moveY, delta);
    }
}
