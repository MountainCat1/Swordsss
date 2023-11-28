using Godot;
using System;

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

    public GameState GameState { get; set; }
    public GameStatus GameStatus { get; set; } = GameStatus.Initial;



    public override void _Ready()
    {
        base._Ready();
        this.GameState = GetNode<GameState>("GameState");
        
        var scoreTimer = GetNode<Timer>("ScoreTimer");
        scoreTimer.WaitTime = 0.1f;
        scoreTimer.Timeout += _OnScoreTimerTimeout;
        scoreTimer.Start();

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

    private void _OnScoreTimerTimeout()
    {
        GameState.AddScore(0.1f);
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