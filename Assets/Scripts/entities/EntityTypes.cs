using System.ComponentModel;

public enum EntityTypes
{
    [Description("Tank")]
    Tank = 1,
    
    [Description("Infantry")]
    Infantry = 2,
    
    [Description("Support")]
    Support = 3,
    
    [Description("AntiArmor")]
    AntiArmor = 4,
    
    [Description("Extra")]
    Extra = 5
}