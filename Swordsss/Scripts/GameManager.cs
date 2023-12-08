using System;
using Godot;
using Timer = Godot.Timer;

namespace Swordsss.Scripts;

public enum GameStatus
{
    Initial,
    Paused,
    Playing,
    Gameover
}

public partial class GameManager : Node
{
    public event Action OnGameEnd;
    
    public static GameManager Instance { get; private set; }

    [Export] public GameConfiguration GameConfiguration { get; set; }

    public GameState GameState { get; set; }
    public GameStatus GameStatus { get; set; } = GameStatus.Initial;
    

    public override void _Ready()
    {
        base._Ready();
        this.GameState = GetNode<GameState>("GameState");
        
        var scoreTimer = GetNode<Timer>("ScoreTimer");
        float scoreTimerWaitTime = 0.1f;
        scoreTimer.WaitTime = scoreTimerWaitTime;
        scoreTimer.Timeout += () => _OnScoreTimerTimeout(scoreTimerWaitTime);
        scoreTimer.Start();
        OnGameEnd += () => scoreTimer.Stop();

        var player = Player.Instance;
        player.Health.Depleted += OnPlayerDeath;

        GameStatus = GameStatus.Playing;
    }
    
    public override void _Notification(int what)
    {
        base._Notification(what);
        if(what == NotificationEnterTree)
            Instance = this;
        if(what == NotificationExitTree)
            Instance = null;
    }

    private void OnPlayerDeath()
    {
        GameStatus = GameStatus.Gameover;
        OnGameEnd?.Invoke();
        GD.Print("GAME OVER");
    }

    private void _OnScoreTimerTimeout(float scoreTimerWaitTime)
    {
        GameState.AddScore(scoreTimerWaitTime * GameConfiguration.ScorePerSecond);
    }

    public void Restart()
    {
        GetTree().ReloadCurrentScene();
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}