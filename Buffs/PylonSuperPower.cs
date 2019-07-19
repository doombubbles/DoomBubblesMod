using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Buffs
{
    public class PylonSuperPower : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Pylon Super Power");
            Description.SetDefault("Increased Life/Mana Regen and Damage");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaRegenBuff = true;
            player.manaRegenBonus += 25;
            player.lifeRegen++;
            
            if (DoomBubblesMod.allDamageLoaded)
            {
                ModLoader.GetMod("AllDamage").Call(new object[] {"Damage", player, .15f});
            }
            else
            {
                player.meleeDamage += .15f;
                player.rangedDamage += .15f;
                player.magicDamage += .15f;
                player.minionDamage += .15f;
                player.thrownDamage += .15f;
            }
        }
    }
}