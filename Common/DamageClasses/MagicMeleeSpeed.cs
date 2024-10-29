namespace DoomBubblesMod.Common.DamageClasses;

public class MagicMeleeSpeed : DamageClass
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("magic damage");
    }

    public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
    {
        if (damageClass.CountsAsClass(Melee))
            return new StatInheritanceData(attackSpeedInheritance: 1f);

        return damageClass == Generic || damageClass.CountsAsClass(Magic) ? StatInheritanceData.Full : StatInheritanceData.None;
    }

    public override bool GetEffectInheritance(DamageClass damageClass) => damageClass.CountsAsClass(Magic);
}