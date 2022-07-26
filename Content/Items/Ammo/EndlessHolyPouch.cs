namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessHolyPouch : EndlessPouch<HolyBullet>
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Summons falling stars on impact");
        SacrificeTotal = 1;
    }
}