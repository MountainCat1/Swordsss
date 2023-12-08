using Godot;

namespace Swordsss.Scenes;

public partial class GameOverScreen : Control
{
    [Export] public Button ExitButton { get; set; }
    [Export] public Button RestartButton { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        
        ExitButton.Pressed += () => Scripts.GameManager.Instance.Quit();
        RestartButton.Pressed += Scripts.GameManager.Instance.Restart;
    }
}