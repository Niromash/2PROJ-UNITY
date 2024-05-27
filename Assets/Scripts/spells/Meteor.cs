using UnityEngine;

public class Meteor : Spell
{
    public Meteor(Side side, GameManager gameManager) : base("Meteor", side, new MeteorStats(), gameManager)
    {
    }
    
    public override void ApplyEffect(Entity entity)
    {
        Debug.Log("Meteor hit: " + entity.GetGameObject().name);
        entity.TakeDamage(GetSpellStats().damage);
    }
}