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
    public int VitalityBonus;
    [Space]
    public float StrengthPercentageBonus;
    public float AgilityPercentageBonus;
    public float IntelligencePercentageBonus;
    public float VitalityPercentageBonus;
    [Space]
    public EquipmentType equipmentType;

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        if (IntelligenceBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        if (VitalityBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));

        if (StrengthPercentageBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentageBonus, StatModType.PercentMult, this));
        if (AgilityPercentageBonus != 0)
            c.Agility.AddModifier(new StatModifier(AgilityPercentageBonus, StatModType.PercentMult, this));
        if (IntelligencePercentageBonus != 0)
            c.Intelligence.AddModifier(new StatModifier(IntelligencePercentageBonus, StatModType.PercentMult, this));
        if (VitalityPercentageBonus != 0)
            c.Vitality.AddModifier(new StatModifier(VitalityPercentageBonus, StatModType.PercentMult, this));
    }
    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Vitality.RemoveAllModifiersFromSource(this);
    }
}
