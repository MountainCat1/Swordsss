using Godot;

namespace Swordsss.Scripts;

public partial class EndGameScreenController : Node
{
    [Export] public PackedScene EndGameScreen { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        GameManager.Instance.OnGameEnd += OnGameEnded;
    }

    private void OnGameEnded()
    {
        var endGameScreen = EndGameScreen.Instantiate();
        AddChild(endGameScreen);
    }
}