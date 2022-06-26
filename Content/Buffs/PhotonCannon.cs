using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.Buffs;

public class PhotonCannon : ModBuff
{
    public override void SetStaticDefaults()
    {
        Description.SetDefault("Make sure they're in Power Fields");
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex)
    {
        var modPlayer = player.GetModPlayer<HotsPlayer>();
        if (player.ownedProjectileCounts[ProjectileType<Projectiles.HotS.PhotonCannon>()] > 0)
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