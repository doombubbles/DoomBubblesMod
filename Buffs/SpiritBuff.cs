using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
	public class SpiritBuff : ModBuff
	{

        public int frostmourne = 0;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Frozen Spirits");
            Description.SetDefault("lol default");
            Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            frostmourne = player.GetModPlayer<DoomBubblesPlayer>(mod).frostmourne;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            int nextUpgrade = 0;
            if (frostmourne < 25)
            {
                nextUpgrade = 25;
            }
            else if (frostmourne < 50)
            {
                nextUpgrade = 50;
            }
            else if (frostmourne < 75)
            {
                nextUpgrade = 75;
            }
            else if (frostmourne < 100)
            {
                nextUpgrade = 100;
            }
            
            if (frostmourne >= 100)
            {
                tip = "Frostmourne Fully Sated";
            }
            else
            {
                tip = frostmourne + "/" + nextUpgrade + " Spirits for next Upgrade";
            }
            
        }
    }
}
