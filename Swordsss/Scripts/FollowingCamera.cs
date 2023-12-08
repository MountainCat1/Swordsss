using System;
using Godot;

namespace Swordsss.Scripts;

public partial class FollowingCamera : Camera2D
{
    public static FollowingCamera Instance { get; set; }
    
    [Export] public Node2D Target { get; set; }
    
    private float _shakeIntensity = 0f;
    private float _shakeDecay = 0.1f;
    private readonly Random rng = new Random();
    
    public FollowingCamera()
    {
        if(Instance != null)
            throw new Exception("Cannot create more than one instance of FollowingCamera");
        
        Instance = this;
    }

    public override void _Ready()
    {
        base._Ready();
        
        Target.TreeExited += () => Target = null;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (Target != null)
            GlobalPosition = Target.GlobalPosition;
        
        if (_shakeIntensity > 0)
        {
            // Apply a random offset to the camera position
            Offset = new Vector2(
                (float)(rng.NextDouble() * 2 - 1) * _shakeIntensity,
                (float)(rng.NextDouble() * 2 - 1) * _shakeIntensity
            );

            // Reduce the shake intensity over time
            _shakeIntensity -= _shakeDecay * (float)delta;
        }
        else
        {
            // Reset the offset when the shaking is over
            Offset = Vector2.Zero;
            _shakeIntensity = 0;
        }
    }
    
    public void StartShake(float intensity, float decay)
    {
        _shakeIntensity = intensity;
        _shakeDecay = decay;
    }
}