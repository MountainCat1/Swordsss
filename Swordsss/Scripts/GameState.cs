using System;
using Godot;

namespace Swordsss.Scripts;

public partial class GameState : Node
{
    public event Action OnScoreChanged;
    
    public DateTime GameStartTime { get; set; }
    public float Time => (float) (DateTime.Now - GameStartTime).TotalSeconds;
    public float Score { get; private set; }

    public void AddScore(float amount)
    {
        Score += amount;
        Score = MathF.Round(Score, 1);
        OnScoreChanged?.Invoke();
    }
}