using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Rage : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rage");
			Description.SetDefault("Increased Movement Speed");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        float yup = player.buffTime[buffIndex] > 120 ? .6f : .2f;
	        player.moveSpeed += yup;
	        player.maxRunSpeed += yup * 10;
	        player.wingTime += yup;
        }
        
	}
}
