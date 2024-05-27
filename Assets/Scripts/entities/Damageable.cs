public interface Damageable : Collidable, HasTeam
{
    void TakeDamage(float damage);
    float GetHealth();
    void Kill();
}

