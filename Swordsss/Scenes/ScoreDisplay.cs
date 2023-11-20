using Godot;
using System;

public partial class ScoreDisplay : Label
{
    public override void _Ready()
    {
        base._Ready();
        GameManager.Instance.GameState.OnScoreChanged += OnScoreChanged;
        OnScoreChanged();
    }

    private void OnScoreChanged()
    {
        Text = "Score: " + GameManager.Instance.GameState.Score.ToString("F1");
    }
}
