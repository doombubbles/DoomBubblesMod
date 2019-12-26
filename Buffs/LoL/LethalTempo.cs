using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs.LoL
{
    public class LethalTempo : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lethal Tempo");
            Description.SetDefault("Mucho Increased Attack Speed");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Mod gottaGoFast = ModLoader.GetMod("GottaGoFast");
            float speed = .3f + .03f * player.GetModPlayer<LoLPlayer>().getLevel();
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

            Dust.NewDustPerfect(player.Center + new Vector2(-20, 20), 57, player.velocity / 2 + new Vector2(-.5f, -2));
            Dust.NewDustPerfect(player.Center + new Vector2(20, 20), 57, player.velocity / 2 + new Vector2(.5f, -2));
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}