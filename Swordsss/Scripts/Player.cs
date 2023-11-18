using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed { get; set; }

    private AnimatedSprite2D _animatedSprite2D;
    
    public override void _Ready()
    {
        base._Ready();

        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        var playerInput = PlayerInput.Instance;
        
        playerInput.OnPlayerMovePhysics += OnPlayerMove;
    }

    private void OnPlayerMove(double delta, Vector2 move)
    {
        Velocity = move * Speed * (float)delta;
        
        MoveAndSlide();
        
        if (move.X < 0)
            _animatedSprite2D.FlipH = true;
        else if(move.X > 0)
            _animatedSprite2D.FlipH = false;
    }
}