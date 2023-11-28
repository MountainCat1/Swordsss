using Godot;
using System;
using Swordsss.Scripts;

public partial class Player : CharacterBody2D, IDamageable
{
    public static Player Instance { get; private set; }
    
    [Export] public float Speed { get; set; }

    
    public IHealth Health { get; private set; }

    private AnimatedSprite2D _animatedSprite2D;

    public Player()
    {
        if(Instance != null)
            throw new Exception("Player already exists");
        
        Instance = this;
    }
    
    public override void _Ready()
    {
        base._Ready();

        Health = GetNode<Health>("Health");
        
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        var playerInput = PlayerInput.Instance;
        
        playerInput.OnPlayerMovePhysics += OnPlayerMove;
    }

    private void OnPlayerMove(double delta, Vector2 direction)
    {
        Velocity = direction * Speed;
        
        MoveAndSlide();
        
        if (direction.X < 0)
            _animatedSprite2D.FlipH = true;
        else if(direction.X > 0)
            _animatedSprite2D.FlipH = false;
    }
}