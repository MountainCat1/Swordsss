using System;
using System.Linq;
using Godot;

namespace Swordsss.Scripts;

[GlobalClass]
[Tool]
public partial class GameConfiguration : Resource
{
    private Resource[] _enemySpawnings = Array.Empty<Resource>();

    [Export]
    public Resource[] EnemySpawnings
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