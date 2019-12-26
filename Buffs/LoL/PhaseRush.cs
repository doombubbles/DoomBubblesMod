using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class PhaseRush : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Phase Rush");
			Description.SetDefault("Increased Movement Speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        player.moveSpeed += .4f + player.GetModPlayer<LoLPlayer>().getLevel() * .04f;
	        player.maxFallSpeed += .4f + player.GetModPlayer<LoLPlayer>().getLevel() * .04f;
	        player.maxRunSpeed += 4f + player.GetModPlayer<LoLPlayer>().getLevel() * .4f;
	        player.wingTime += .4f + player.GetModPlayer<LoLPlayer>().getLevel() * .04f;
        }

    }
}
