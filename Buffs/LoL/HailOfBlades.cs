using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
    public class HailOfBlades : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Hail of Blades");
            Description.SetDefault("Super bonus attack speed");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Mod gottaGoFast = ModLoader.GetMod("GottaGoFast");
            float speed = .5f;
            if(gottaGoFast != null)
            {
                //First Argument is a string for the type; either "magicSpeed", "rangedSpeed" or "attackSpeed"
                //Second Argument is an int for the index of the player in question; You can get that using player.whoAmI
                //Third argument is a float for the value to add; e.g. .1f for 10% increase, -.05f for 5% decrease
                gottaGoFast.Call("attackSpeed", player.whoAmI, speed);
            } else {
                //If the player doesn't have the mod, just increase melee speed
                player.meleeSpeed += speed;
            }

            int i = player.buffTime[buffIndex] % 360;
            Dust d = Dust.NewDustPerfect(player.Center, 182, player.velocity / 2 + 2 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), (float) Math.Sin(Math.PI * i / 180.0)));
            d.noGravity = true;
            i = (player.buffTime[buffIndex] + 120) % 360;
            d = Dust.NewDustPerfect(player.Center, 182, player.velocity / 2 + 2 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), (float) Math.Sin(Math.PI * i / 180.0)));
            d.noGravity = true;
            i = (player.buffTime[buffIndex] + 240) % 360;
            d = Dust.NewDustPerfect(player.Center, 182, player.velocity / 2 + 2 * new Vector2((float) Math.Cos(Math.PI * i / 180.0), (float) Math.Sin(Math.PI * i / 180.0)));
            d.noGravity = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}