using Godot;

namespace Swordsss.Scripts;

[GlobalClass]
public partial class PickupSpawningConfig : Resource
{
    [Export] public float SpawningRate { get; set; }
    [Export] public PackedScene PickupPrefabScene { get; set; }
}