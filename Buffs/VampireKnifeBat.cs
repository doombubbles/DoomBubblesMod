using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class VampireKnifeBat : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Knife Bat");
            Description.SetDefault("Hi Oliver");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var modPlayer = player.GetModPlayer<DoomBubblesPlayer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.VampireKnifeBat>()] > 0)
            {
                modPlayer.vampireKnifeBat = true;
            }

            if (!modPlayer.vampireKnifeBat)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}