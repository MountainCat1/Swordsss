using Godot;
using System;
using Swordsss.Scripts;

public partial class Player : Creature
{
    public static Player Instance { get; private set; }
    
    public Player()
    {
        if(Instance != null)
            throw new Exception("Player already exists");
        
        Instance = this;
    }
    
    public override void _Ready()
    {
        base._Ready();

        var playerInput = PlayerInput.Instance;
        
        playerInput.OnPlayerMovePhysics += OnPlayerMove;
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