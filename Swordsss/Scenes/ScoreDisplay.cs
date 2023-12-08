using Godot;

namespace Swordsss.Scenes;

public partial class ScoreDisplay : Label
{
    public override void _Ready()
    {
        base._Ready();
        Scripts.GameManager.Instance.GameState.OnScoreChanged += OnScoreChanged;
        OnScoreChanged();
    }

    private void OnScoreChanged()
    {
        Text = "Score: " + Scripts.GameManager.Instance.GameState.Score.ToString("F1");
    }
}