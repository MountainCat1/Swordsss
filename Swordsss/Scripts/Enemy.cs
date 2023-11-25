using System;
using Godot;

namespace Swordsss.Scripts;

public partial class Enemy : Creature, IWeaponHolder
{
    public IWeapon Weapon { private set; get; }

    [Export] public int ScoreReward { get; set; }

    private AnimatedSprite2D _animatedSprite2D;
    private ProgressBar _progressBar;
    private AnimationPlayer _damagedAnimationPlayer;

    private bool _walkCooldown = false;

    public override void _Ready()
    {
        base._Ready();

        _progressBar = GetNode<ProgressBar>("Control/ProgressBar");
        _damagedAnimationPlayer = GetNode<AnimationPlayer>("DamagedAnimationPlayer");

        _progressBar.MaxValue = Health.Max;

        Health.Changed += OnHealthChanged;
        Health.Depleted += Kill;

        Weapon = GetNode<IWeapon>("Weapon");
        Weapon.CooldownEnded += OnAttackCooldownEnded;

        GetNode<EnemyAnimator>("AnimatedSprite2D").Register(this);

        Health.Damaged += OnDamaged;

        OnKilled += () => GameManager.Instance.GameState.AddScore(ScoreReward);
    }

    private void OnDamaged()
    {
        _damagedAnimationPlayer.Seek(0);
        _damagedAnimationPlayer.Play("damaged_animation");
    }

    private void OnAttackCooldownEnded()
    {
        _walkCooldown = false;
    }

    private void OnHealthChanged()
    {
        _progressBar.Value = Health.Amount;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        var player = GetPlayer();

        ApplyMomentum();
        
        if (Weapon.CanAttack(this, player))
        {
            Weapon.Attack(player);
            _walkCooldown = true;
        }
        else if (!_walkCooldown)
        {
            SteerMovement(player.GlobalPosition);
        }
        
        MoveAndSlide();
    }

    private Player GetPlayer()
    {
        return Player.Instance;
    }
}