using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class FleetFootwork : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fleet Footwork");
			Description.SetDefault("Increased Movement Speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        
	        player.moveSpeed *= 1.3f;
	        player.maxFallSpeed *= 1.3f;
	        player.maxRunSpeed *= 1.3f;
	        player.wingTime += .3f;
	        
	        if (player.buffTime[buffIndex] < 120 && player.buffTime[buffIndex] > 115) player.velocity *= 1.3f;
        }

    }
}
