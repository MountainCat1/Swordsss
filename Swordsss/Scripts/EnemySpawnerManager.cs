using Godot;

namespace Swordsss.Scripts;

public partial class EnemySpawnerManager : Node
{
    [Export] public PackedScene EnemySpawnerPrefab;
    
    public override void _Ready()
    {
        base._Ready();

        var configuration = GameManager.Instance.GameConfiguration;

        foreach (var enemySpawning in configuration.EnemySpawnings)
        {
            var spawner = new EnemySpawner();

            spawner.EnemyPrefab = enemySpawning.EnemyScenePrefab;
            spawner.SpawnRate = enemySpawning.TargetSpawnRate;
            spawner.SpawningStartTime = enemySpawning.SpawingStartTime;
            spawner.SpawnRange = 200f;
            
            AddChild(spawner);
        }
    }
}