using Godot;
using System;
using System.Linq;

[GlobalClass]
[Tool]
public partial class GameConfiguration : Resource
{
    private EnemySpawning[] _enemySpawnings = Array.Empty<EnemySpawning>();

    [Export]
    public EnemySpawning[] EnemySpawnings
    {
        get => _enemySpawnings;
        set
        {
            _enemySpawnings = value.Select(x => x ?? new EnemySpawning()).ToArray();
            foreach (var enemySpawning in _enemySpawnings)
            {
                enemySpawning.ResourceName = "Spawning Config";
            }
        }
    }
    
    [Export] public float ScorePerSecond { get; set; }
}