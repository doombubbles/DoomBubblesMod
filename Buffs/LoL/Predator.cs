using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Predator : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Predator");
			Description.SetDefault("Increased Movement Speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        player.moveSpeed += .5f + player.GetModPlayer<LoLPlayer>().getLevel() * .05f;
	        player.maxFallSpeed += .5f + player.GetModPlayer<LoLPlayer>().getLevel() * .05f;
	        player.maxRunSpeed += 5f + player.GetModPlayer<LoLPlayer>().getLevel() * .1f;
	        player.wingTime += .5f + player.GetModPlayer<LoLPlayer>().getLevel() * .05f;
        }

    }
}
