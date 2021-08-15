using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class MeleeMagic : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("melee/magic damage");
        }

        protected override float GetBenefitFrom(DamageClass damageClass)
        {
            return damageClass == Melee || damageClass == Magic || damageClass == Generic ? 1 : 0;
        }

        public override bool CountsAs(DamageClass damageClass)
        {
            return damageClass == Melee || damageClass == Magic;
        }
    }
}