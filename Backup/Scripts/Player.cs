using Godot;
using System;
using Godot.NativeInterop;
using Array = Godot.Collections.Array;

public partial class Player : Node2D
{
    [Export] public float Speed { get; set; } = 200f;
    [Export] public Texture2D Texture2D { get; set; } = null;
    
    private PlayerInput _playerInput;
    private AnimatedSprite2D _animatedSprite2D;

    public override void _Ready()
    {
        base._Ready();

        _playerInput = PlayerInput.Instance ?? throw new Exception("PlayerInput is not initialized");
        
        _playerInput.PlayerMoved += OnPlayerMoved; 
        _playerInput.PlayerAttacked += OnPlayerAttacked;
        
                
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    private void OnPlayerAttacked()
    {
        var sprite = new Sprite2D();
        AddChild(sprite);

        sprite.Texture = Texture2D;
        
        GD.Print("XD");

        var random = new RandomNumberGenerator();
        
        sprite.Position = new Vector2(500 * random.RandfRange(-1, 1), 500 * random.RandfRange(-1, 1 ));
    }

    private void OnPlayerMoved(float x, float y, double delta)
    {
        float multiplier = (float) delta * Speed;
        
        MoveLocalX(x * multiplier);
        MoveLocalY(y * multiplier);
        
        if(x > 0)
            _animatedSprite2D.FlipH = false;
        if(x < 0)
            _animatedSprite2D.FlipH = true;
    }
}
