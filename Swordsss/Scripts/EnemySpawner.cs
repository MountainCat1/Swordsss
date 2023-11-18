using Godot;

namespace Swordsss.Scripts;

public partial class EnemySpawner : Node2D
{
    [Export] public PackedScene EnemyPrefab;

    [Export] public float SpawnRate = 2.0f;
    
    [Export] public float SpawnRange = 500.0f;

    private Timer _spawnTimer;

    public override void _Ready()
    {
        _spawnTimer = GetNode<Timer>("SpawnTimer");
        _spawnTimer.WaitTime = 1f / SpawnRate;
        _spawnTimer.Timeout += _OnSpawnTimerTimeout;
        _spawnTimer.Start();
    }

    private void _OnSpawnTimerTimeout()
    {
        var enemy = (Enemy)EnemyPrefab.Instantiate();
        var spawnPosition = GetSpawnPoint();
        enemy.GlobalPosition = spawnPosition;
        AddChild(enemy);
    }

    private Vector2 GetSpawnPoint()
    {
        return new Vector2(GD.Randf() * SpawnRange, GD.Randf() * SpawnRange);
    }
}