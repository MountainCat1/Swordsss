using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public GameState GameState { get; set; }

    public GameManager()
    {
        if (Instance != null)
            throw new Exception("Player already exists");

        Instance = this;
    }

    public override void _Ready()
    {
        base._Ready();
        this.GameState = GetNode<GameState>("GameState");
        
        var scoreTimer = GetNode<Timer>("ScoreTimer");
        scoreTimer.WaitTime = 0.1f;
        scoreTimer.Timeout += _OnScoreTimerTimeout;
        scoreTimer.Start();
    }
    
    private void _OnScoreTimerTimeout()
    {
        GameState.AddScore(0.1f);
    }
}