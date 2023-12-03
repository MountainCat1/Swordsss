using System;
using Godot;

namespace Swordsss.Scripts;

public partial class EnemySpawnerManager : Node
{
    [Export] public PackedScene EnemySpawnerPrefab;

    public override void _Ready()
    {
        base._Ready();

        var configuration = GameManager.Instance.GameConfiguration;

        foreach (var enemySpawningResource in configuration.EnemySpawnings)
        {
            var enemySpawing = enemySpawningResource as EnemySpawning
                               ?? throw new InvalidCastException("Cannot cast the resourc to EnemySpawning");

            var spawner = new EnemySpawner();

            spawner.EnemyPrefab = enemySpawing.EnemyScenePrefab;
            spawner.SpawnRate = enemySpawing.TargetSpawnRate;
            spawner.SpawningStartTime = enemySpawing.SpawingStartTime;
            spawner.SpawnRange = 200f;

            AddChild(spawner);
        }
    }
}