using Godot;

public partial class EnemySpawning : Resource
{
    [Export] public PackedScene EnemyScenePrefab { get; set; }
    [Export] public double TargetSpawnRate { get; set; }
    [Export] public double SpawingStartTime { get; set; }
}