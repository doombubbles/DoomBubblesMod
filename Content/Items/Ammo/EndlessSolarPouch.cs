namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessSolarPouch : EndlessPouch<SolarBullet>
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("Deals bonus damage to airborne enemies");
        Item.ResearchUnlockCount = 1;
    }
}