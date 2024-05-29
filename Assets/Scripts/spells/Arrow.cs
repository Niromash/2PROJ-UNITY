using UnityEngine;

public class Arrow : Spell
{
    public Arrow(Side side, GameManager gameManager) : base("Arrow", side, new ArrowStats(), gameManager)
    {
    }

    public override void ApplyEffect(Entity entity)
    {
        if (entity.GetTeam().GetSide().Equals(GetSide())) return;

        Debug.Log("Arrow hit: " + entity.GetGameObject().name);
        entity.TakeDamage(GetSpellStats().damage);
    }
}