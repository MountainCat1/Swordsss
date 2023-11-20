using System.Data;
using Godot;

namespace Swordsss.Scripts;

public partial class EnemyAnimator : AnimatedSprite2D
{
    [Export] public string WalkAnimation { get; set; }
    [Export] public string AttackAnimation { get; set; }

    public void Register(Enemy enemy)
    {
        enemy.Weapon.Attacked += OnAttack;

        AnimationFinished += () =>
        {
            if (Animation == AttackAnimation)
                Play(WalkAnimation);
        };
    }

    private void OnAttack()
    {
        Play(AttackAnimation);
    }
}