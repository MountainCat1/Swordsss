using System;
using Godot;

namespace Swordsss.Scripts;

public enum Sounds
{
    Pickup,
    HitSound,
    PlayerHitSound,
    SpitSound,
    ChompSound,
    EnemyDiedSound
}

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }

    public AudioManager()
    {
        Instance = this;
    }
    
    [Export] public AudioStream PickupSound { get; set; }
    [Export] public AudioStream HitSound { get; set; }
    [Export] public AudioStream PlayerHitSound { get; set; }
    [Export] public AudioStream SpitSound { get; set; }
    [Export] public AudioStream ChompSound { get; set; }
    [Export] public AudioStream EnemyDiedSound { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
    }

    public static void Play(Vector2 position, Sounds sounds)
    {
        switch (sounds)
        {
            case Sounds.Pickup:
                Instance.Play(position, Instance.PickupSound);
                break;
            case Sounds.HitSound:
                Instance.Play(position, Instance.HitSound);
                break;
            case Sounds.PlayerHitSound:
                Instance.Play(position, Instance.PlayerHitSound);
                break;
            case Sounds.SpitSound:
                Instance.Play(position, Instance.SpitSound);
                break;
            case Sounds.ChompSound:
                Instance.Play(position, Instance.ChompSound);
                break;
            case Sounds.EnemyDiedSound:
                Instance.Play(position, Instance.EnemyDiedSound);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sounds), sounds, null);
        }
    }

    private void Play(Vector2 position, AudioStream audioStream)
    {
        var audioPlayer = new AudioStreamPlayer2D();
        audioPlayer.Stream = audioStream;
        audioPlayer.GlobalPosition = position;
        AddChild(audioPlayer);
        audioPlayer.Play();
        audioPlayer.Finished += () => audioPlayer.QueueFree();
    }
}