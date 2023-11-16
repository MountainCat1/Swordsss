namespace Swordsss.Scripts;

public interface IDamageable
{
    public IHealth Health { get; }
}

public interface IHealth
{
    public float Amount { get; set; }
    public float Max { get; set; }
    void DealDamage(int i);
}

public class Health : IHealth
{
    public float Amount { get; set; }
    public float Max { get; set; }
    public void DealDamage(int i)
    {
        throw new System.NotImplementedException();
    }
}