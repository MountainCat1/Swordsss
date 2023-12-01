using Godot;

namespace Swordsss.Scripts;

public partial class HealthPickup : Pickup
{
    [Export] public int HealingAmount { get; set; }
    [Export] public int ScoreGain { get; set; }
    
    
    protected override void OnPickedUp()
    {
        base.OnPickedUp();
     
        Player.Instance.Health.Heal(HealingAmount);
        GameManager.Instance.GameState.AddScore(ScoreGain);
    }
}