using UnityEngine;

public class PlasticMeteor : Spell
{
    public PlasticMeteor(Team team) : base("PlasticMeteor", team, new PlasticMeteorStats())
    {
    }

    public override void ApplyEffect(Entity entity)
    {
        if (entity.GetTeam().GetSide().Equals(GetTeam().GetSide())) return;

        entity.TakeDamage(this);
    }
}