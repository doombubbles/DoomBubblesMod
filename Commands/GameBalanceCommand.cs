using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace DoomBubblesMod.Commands
{
	public class GameBalanceCommand : ModCommand
	{
		public override CommandType Type
		{
			get { return CommandType.World; }
		}

		public override string Command
		{
			get {
                return "gamebalance";
            }
		}

		public override string Usage
		{
			get {
                return "/gamebalance allNpcHPMult bossHPMult enemyDmgMult";
            }
		}

		public override string Description 
		{
			get { return "Changes the HP and Damage of enemies and bosses"; }
		}

		public override void Action(CommandCaller caller, string input, string[] args)
		{
            double enemyOld = DoomBubblesWorld.enemyHP;
            double bossOld = DoomBubblesWorld.bossHP;
            double dmgOld = DoomBubblesWorld.mobDMG;

            if (args.Length > 0)
            {
                DoomBubblesWorld.enemyHP = double.Parse(args[0]);
            }
            if (args.Length > 1)
            {
                DoomBubblesWorld.bossHP = double.Parse(args[1]);
            }
            if (args.Length > 2)
            {
                DoomBubblesWorld.mobDMG = double.Parse(args[2]);
            }


            string key = "Enemy HP Mult: " + DoomBubblesWorld.enemyHP + "x (was " + enemyOld + "x) Boss HP Mult: " + DoomBubblesWorld.bossHP + "x (was " + bossOld + "x) Mob Damage Mult: " + DoomBubblesWorld.mobDMG + "x (was " + dmgOld + "x)";
            Main.NewText(key, Color.Orange);
            
            
        }
	}
}