using Godot;

namespace Swordsss.Scripts;

public partial class EnemySpawner : Node2D
{
    public double SpawnRate { get; set; }
    public PackedScene EnemyPrefab { get; set; }
    public float SpawnRange { get; set; }
    public double SpawningStartTime { get; set; }
    
    private Timer _spawnTimer;
    
    public override void _Ready()
    {
        _spawnTimer = new Timer();
        AddChild(_spawnTimer);
        
        _spawnTimer.WaitTime = SpawningStartTime;
        _spawnTimer.Timeout += StartSpawning;
        _spawnTimer.Start();
        
        // When game ends, stop spawning
        GameManager.Instance.OnGameEnd += () => _spawnTimer.Stop();
    }

    private void StartSpawning()
    {
        _spawnTimer.WaitTime = 1f / SpawnRate;
        _spawnTimer.Timeout -= StartSpawning;
        _spawnTimer.Timeout += SpawnEnemy;
        GD.Print($"Started spawing!");
    }

    private void SpawnEnemy()
    {
        var enemy = (Enemy)EnemyPrefab.Instantiate();
        var spawnPosition = GetSpawnPoint();
        enemy.GlobalPosition = spawnPosition;
        AddChild(enemy);
    }
    
    private Vector2 GetSpawnPoint()
    {
        var playerPosition = Player.Instance.GlobalPosition;
        
        return new Vector2()
        {
            X = playerPosition.X + (float)GD.RandRange(-1f, 1f) * SpawnRange, 
            Y = playerPosition.Y + (float)GD.RandRange(-1f, 1f) * SpawnRange
        };
    }
}