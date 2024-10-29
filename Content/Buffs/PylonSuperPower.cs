namespace DoomBubblesMod.Content.Buffs;

public class PylonSuperPower : ModBuff
{
    public override void SetStaticDefaults()
    {
        // Description.SetDefault("Increased Life/Mana Regen and Damage");
        Main.debuff[Type] = false;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        BuffID.Sets.LongerExpertDebuff[Type] = false;
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

        player.GetDamage(DamageClass.Generic) += .15f;
    }
}