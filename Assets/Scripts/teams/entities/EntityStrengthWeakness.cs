using System;
using System.Collections.Generic;

public class EntityStrengthWeakness
{
    private Dictionary<Tuple<EntityTypes, EntityTypes>, float> strengthWeaknessTable;

    public EntityStrengthWeakness()
    {
        strengthWeaknessTable = new Dictionary<Tuple<EntityTypes, EntityTypes>, float>();

        SetStrengthWeakness(EntityTypes.Tank, EntityTypes.Tank, 1.0f);
        SetStrengthWeakness(EntityTypes.Tank, EntityTypes.Infantry, 1.1f);
        SetStrengthWeakness(EntityTypes.Tank, EntityTypes.Support, 1.1f);
        SetStrengthWeakness(EntityTypes.Tank, EntityTypes.AntiArmor, 0.8f);
        SetStrengthWeakness(EntityTypes.Tank, EntityTypes.Extra, 1.0f);

        SetStrengthWeakness(EntityTypes.Infantry, EntityTypes.Tank, 0.8f);
        SetStrengthWeakness(EntityTypes.Infantry, EntityTypes.Infantry, 1.0f);
        SetStrengthWeakness(EntityTypes.Infantry, EntityTypes.Support, 1.1f);
        SetStrengthWeakness(EntityTypes.Infantry, EntityTypes.AntiArmor, 0.8f);
        SetStrengthWeakness(EntityTypes.Infantry, EntityTypes.Extra, 1.0f);

        SetStrengthWeakness(EntityTypes.Support, EntityTypes.Tank, 0.8f);
        SetStrengthWeakness(EntityTypes.Support, EntityTypes.Infantry, 1.1f);
        SetStrengthWeakness(EntityTypes.Support, EntityTypes.Support, 1.0f);
        SetStrengthWeakness(EntityTypes.Support, EntityTypes.AntiArmor, 1.1f);
        SetStrengthWeakness(EntityTypes.Support, EntityTypes.Extra, 1.0f);

        SetStrengthWeakness(EntityTypes.AntiArmor, EntityTypes.Tank, 1.2f);
        SetStrengthWeakness(EntityTypes.AntiArmor, EntityTypes.Infantry, 0.8f);
        SetStrengthWeakness(EntityTypes.AntiArmor, EntityTypes.Support, 0.8f);
        SetStrengthWeakness(EntityTypes.AntiArmor, EntityTypes.AntiArmor, 1.0f);
        SetStrengthWeakness(EntityTypes.AntiArmor, EntityTypes.Extra, 1.0f);

        SetStrengthWeakness(EntityTypes.Extra, EntityTypes.Tank, 1.0f);
        SetStrengthWeakness(EntityTypes.Extra, EntityTypes.Infantry, 1.0f);
        SetStrengthWeakness(EntityTypes.Extra, EntityTypes.Support, 1.0f);
        SetStrengthWeakness(EntityTypes.Extra, EntityTypes.AntiArmor, 1.0f);
        SetStrengthWeakness(EntityTypes.Extra, EntityTypes.Extra, 1.0f);
    }

    private void SetStrengthWeakness(EntityTypes sourceType, EntityTypes targetType, float factor)
    {
        strengthWeaknessTable[Tuple.Create(sourceType, targetType)] = factor;
    }

    public float GetStrengthWeakness(EntityTypes sourceType, EntityTypes targetType)
    {
        if (strengthWeaknessTable.TryGetValue(Tuple.Create(sourceType, targetType), out float factor))
        {
            return factor;
        }
        else
        {
            // Return 1f if no strength or weakness is defined
            return 1f;
        }
    }

    public EntityTypes GetStrongest(EntityTypes targetType)
    {
        EntityTypes strongest = EntityTypes.Tank;
        float maxFactor = 0f;
        foreach (EntityTypes sourceType in Enum.GetValues(typeof(EntityTypes)))
        {
            float factor = GetStrengthWeakness(sourceType, targetType);
            if (factor > maxFactor)
            {
                maxFactor = factor;
                strongest = sourceType;
            }
        }

        return strongest;
    }
}