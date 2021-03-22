using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class PhotonCannon : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Photon Cannon");
            Description.SetDefault("Make sure they're in Power Fields");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var modPlayer = player.GetModPlayer<HotSPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("PhotonCannon")] > 0)
            {
                modPlayer.photonCannon = true;
            }

            if (!modPlayer.photonCannon)
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