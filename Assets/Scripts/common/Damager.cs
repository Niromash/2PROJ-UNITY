public interface Damager : Nameable
{
    public Team GetTeam();
    public DamagerStats GetDamagerStats();
}

public interface DamagerStats
{
    public float GetDamage();
}