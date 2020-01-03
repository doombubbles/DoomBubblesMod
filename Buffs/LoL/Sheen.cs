using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Sheen : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Spellblade");
			Description.SetDefault("Bonus melee damage");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        
        }
        
	}
}