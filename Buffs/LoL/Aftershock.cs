using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
	public class Aftershock : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Grasp of the Undying");
			Description.SetDefault("");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
			longerExpertDebuff = false;
		}

        public override void Update(Player player, ref int buffIndex)
        {
	        LoLPlayer loLPlayer = player.GetModPlayer<LoLPlayer>();

	        Dust.NewDust(player.position, player.width, player.height, mod.DustType("Green182"));
	        
	        if (player.buffTime[buffIndex] == 1)
	        {
		        for (var i = 0; i < Main.npc.Length; i++)
		        {
			        var npc = Main.npc[i];

			        var distance = npc.Distance(player.Center);
			        if (npc.active && !npc.dontTakeDamage && !npc.townNPC && Main.myPlayer == player.whoAmI && distance <= 200)
			        {
				        player.ApplyDamageToNPC(npc, loLPlayer.Adapative(25 + 5 * loLPlayer.getLevel() + player.statDefense), 0f, 0, false);
			        }
		        }
		        
		        for (int i = 0; i < 360; i++)
		        {
			        Dust d = Dust.NewDustPerfect(
				        player.Center, mod.DustType("Green182"), player.velocity / 2 + 10 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
					                                                 (float) Math.Sin(Math.PI * i / 180.0)));
			        d.noGravity = true;
		        }
	        
		        for (int i = 0; i < 360; i++)
		        {
			        Dust d = Dust.NewDustPerfect(
				        player.Center + 200 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
					        (float) Math.Sin(Math.PI * i / 180.0)), 
				        mod.DustType("Green182"), player.velocity / 2 + -10 * new Vector2((float) Math.Cos(Math.PI * i / 180.0),
					                                  (float) Math.Sin(Math.PI * i / 180.0)));
			        d.noGravity = true;
		        }
	        }
        }
        
        

    }
}
