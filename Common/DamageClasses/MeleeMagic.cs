namespace DoomBubblesMod.Common.DamageClasses;

public class MeleeMagic : DamageClass
{
    public override void SetStaticDefaults()
    {
        ClassName.SetDefault("melee/magic damage");
    }

    public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
    {
        return damageClass == Melee || damageClass == Magic || damageClass == Generic ? StatInheritanceData.Full : StatInheritanceData.None;
    }

    public override bool GetEffectInheritance(DamageClass damageClass)
    {
        return damageClass == Melee || damageClass == Magic;
    }
}