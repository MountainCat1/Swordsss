using Godot;

namespace Swordsss.Scripts;

public partial class UI : CanvasLayer
{
    [Export] public ProgressBar HealthBar;
    
    public override void _Ready()
    {
        base._Ready();

        var player = Player.Instance;
        
        player.Health.Changed += OnHealthChanged;
        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        HealthBar.Value = Player.Instance.Health.Amount;
        HealthBar.MaxValue = Player.Instance.Health.Max;
    }
}