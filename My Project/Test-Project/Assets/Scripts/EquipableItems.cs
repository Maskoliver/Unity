using UnityEngine;
public enum EquipmentType
{
    Helmet,
    Chest,
    Shoulders,
    Gloves,
    Pants,
    Boots,
    Weapeon_1,
    Weapeon_2,
    Accesory_1,
    Accesory_2,
}
[CreateAssetMenu]
public class EquipableItems : Item
{

    public int StrengthBonus;
    public int AgilityBonus;
    public int IntelligenceBonus;
    public int VitalityhBonus;
    [Space]
    public float StrengthPercentageBonus;
    public float AgilityPercentageBonus;
    public float IntelligencePercentageBonus;
    public float VitalityPercentageBonus;
    [Space]
    public EquipmentType equipmentType;
}
