using System.Linq;
using Godot;

namespace Swordsss.Scripts;

[Tool]
public partial class PickupSpawningManager : Node
{
    private Resource[] _spawningConfigs;

    [Export]
    public Resource[] SpawningConfigs
    {
        get => _spawningConfigs;
        set
        {
            _spawningConfigs = value.Select(x => x ?? new PickupSpawningConfig()).ToArray();
            foreach (var enemySpawning in _spawningConfigs)
            {
                enemySpawning.ResourceName = "Pickup Config";
            }
        }
    }

    [Export] public float SpawnRange { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        foreach (var config in SpawningConfigs.Cast<PickupSpawningConfig>())
        {
            var timer = new Timer {WaitTime = 1f / config.SpawningRate};
            timer.Timeout += () =>
            {
                SpawnPickup(config);
            };
            AddChild(timer);
            GD.Print(timer.WaitTime);
            timer.Start();
        }
    }
    
    private void SpawnPickup(PickupSpawningConfig config)
    {
        GD.Print("Spawning pickup!");
        
        var playerPosition = Player.Instance.GlobalPosition;
        var spawnPosition = new Vector2()
        {
            X = playerPosition.X + (float)GD.RandRange(-1f, 1f) * SpawnRange, 
            Y = playerPosition.Y + (float)GD.RandRange(-1f, 1f) * SpawnRange
        };
        
        var pickup = config.PickupPrefabScene.Instantiate<Pickup>();
        pickup.GlobalPosition = spawnPosition;

        AddChild(pickup);
    }
}