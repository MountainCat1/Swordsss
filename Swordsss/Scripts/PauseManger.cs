using System.Transactions;
using Godot;

namespace Swordsss.Scripts;

public partial class PauseManger : Node
{
    [Export] public Node WorldNode { get; set; }
    [Export] public Control PauseUI { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        
        PlayerInput.Instance.OnPlayerPause += OnPlayerPause;
    }

    private void OnPlayerPause()
    {

        if (GameManager.Instance.GameStatus == GameStatus.Playing)
            PauseGame();
        else if (GameManager.Instance.GameStatus == GameStatus.Paused) ResumeGame();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    private void ResumeGame()
    {
        GD.Print("Resuming game...");
        GameManager.Instance.GameStatus = GameStatus.Playing;
        WorldNode.GetTree().Paused = false;
        PauseUI.Hide();
    }

    private void PauseGame()
    {
        GD.Print("Pausing game...");
        GameManager.Instance.GameStatus = GameStatus.Paused;
        WorldNode.GetTree().Paused = true;
        PauseUI.Show();
    }
}