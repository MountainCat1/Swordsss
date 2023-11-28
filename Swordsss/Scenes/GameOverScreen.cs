using Godot;
using System;

public partial class GameOverScreen : Control
{
    [Export] public Button ExitButton { get; set; }
    [Export] public Button RestartButton { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        
        ExitButton.Pressed += () => GameManager.Instance.Quit();
        RestartButton.Pressed += GameManager.Instance.Restart;
    }
}
