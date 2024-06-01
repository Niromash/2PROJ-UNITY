using UnityEngine;

public class Arrow : Spell
{
    public Arrow(Team team) : base("Arrow", team, new ArrowStats())
    {
    }

    public override void ApplyEffect(Entity entity)
    {
        if (entity.GetTeam().GetSide().Equals(GetTeam().GetSide())) return;

        entity.TakeDamage(this);
    }
}