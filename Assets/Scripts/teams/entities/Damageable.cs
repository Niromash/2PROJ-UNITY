public interface Damageable : Collidable, HasTeam
{
    void TakeDamage(Damager damager);
    float GetHealth();
    void Kill(Damager damager);
}

