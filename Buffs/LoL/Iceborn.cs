using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Iceborn : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Iceborn");
			Description.SetDefault("Icy Zone inbound");
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
