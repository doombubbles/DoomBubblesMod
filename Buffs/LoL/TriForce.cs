using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class TriForce : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Spellblade");
			Description.SetDefault("Mucho bonus swing damage");
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
