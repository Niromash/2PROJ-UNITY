using UnityEngine;

public class Meteor : Spell
{
    public Meteor(Team team) : base("Meteor", team, new MeteorStats())
    {
    }

    public override void ApplyEffect(Entity entity)
    {
        if (entity.GetTeam().GetSide().Equals(GetTeam().GetSide())) return;

        Debug.Log("Meteor hit: " + entity.GetGameObject().name);
        entity.TakeDamage(this);
    }
}