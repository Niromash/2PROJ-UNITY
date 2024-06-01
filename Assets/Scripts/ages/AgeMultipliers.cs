public interface AgeMultipliers
{
    float GetGoldMultiplier();
    float GetExperienceMultiplier();
    Entities GetEntitiesStatsMultiplier();
    Turrets GetTurretsStatsMultiplier();

    public class Entities
    {
        public float maxHealth = 1;
        public float damagePerSecond = 1;
        public float blockPerSecondMovementSpeed = 1;
        public float range = 1;
        public float deploymentTime = 1;
    }

    public class Turrets
    {
        public float damagePerSecond = 1;
        public float range = 1;
        public float bulletSpeed = 1;
    }
}