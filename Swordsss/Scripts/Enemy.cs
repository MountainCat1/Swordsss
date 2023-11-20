using System;
using Godot;

namespace Swordsss.Scripts;


public partial class Enemy : CharacterBody2D, IDamageable, IWeaponHolder
{
    
    public IHealth Health { private set; get; }
    
    public IWeapon Weapon { private set; get; }
    
    [Export] public float Speed { get; set; }
    [Export] public int ScoreReward { get; set; }

    private AnimatedSprite2D _animatedSprite2D;
    private ProgressBar _progressBar;
    
    private bool _walkCooldown = false;

    public override void _Ready()
    {
        base._Ready();
        
        Health = GetNode<IHealth>("Health");
        
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _progressBar = GetNode<ProgressBar>("Control/ProgressBar");

        _progressBar.MaxValue = Health.Max;
        
        Health.Changed += OnHealthChanged;
        Health.Depleted += Kill;

        Weapon = GetNode<IWeapon>("Weapon");
        Weapon.CooldownEnded += OnAttackCooldownEnded;
        
        GetNode<EnemyAnimator>("AnimatedSprite2D").Register(this);
    }

    private void OnAttackCooldownEnded()
    {
        _walkCooldown = false;
    }

    private void Kill()
    {
        GameManager.Instance.GameState.AddScore(ScoreReward);
        QueueFree();
    }

    private void OnHealthChanged()
    {
        _progressBar.Value = Health.Amount;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        var player = GetPlayer();
        
        if (Weapon.CanAttack(this, player))
        {
            Weapon.Attack(player);
            _walkCooldown = true;
        }
        else if(!_walkCooldown)
        {
            MovementTo(player.GlobalPosition);
        }
    }

    private void MovementTo(Vector2 target)
    {
        var move = (target - GlobalPosition).Normalized();
        
        Velocity = move * Speed;
        
        MoveAndSlide();
        
        if (move.X < 0)
            _animatedSprite2D.FlipH = true;
        else if(move.X > 0)
            _animatedSprite2D.FlipH = false;
    }
    
    private Player GetPlayer()
    {
        return Player.Instance;
    }

}