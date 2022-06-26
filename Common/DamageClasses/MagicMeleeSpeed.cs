namespace DoomBubblesMod.Common.DamageClasses;

public class MagicMeleeSpeed : DamageClass
{
    public override void SetStaticDefaults()
    {
        ClassName.SetDefault("magic damage");
    }

    public override StatInheritanceData GetModifierInheritance(
        DamageClass damageClass)
    {
        if (damageClass == Melee)
            return new StatInheritanceData(attackSpeedInheritance: 1f);

        return damageClass == Generic || damageClass == Magic ? StatInheritanceData.Full : StatInheritanceData.None;
    }

    public override bool GetEffectInheritance(DamageClass damageClass) => damageClass == Magic;
}