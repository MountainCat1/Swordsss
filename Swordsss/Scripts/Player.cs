using Godot;
using System;

public partial class Player : Node2D
{
    [Export] public float Speed { get; set; }

    private AnimatedSprite2D _animatedSprite2D;
    
    public override void _Ready()
    {
        base._Ready();

        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        var playerInput = PlayerInput.Instance;
        
        playerInput.OnPlayerMove += OnPlayerMove;
    }

    private void OnPlayerMove(double delta, Vector2 move)
    {
        var step = move * Speed * (float)delta;
        Translate(step);

        if (move.X < 0)
            _animatedSprite2D.FlipH = true;
        else if(move.X > 0)
            _animatedSprite2D.FlipH = false;
    }
}
