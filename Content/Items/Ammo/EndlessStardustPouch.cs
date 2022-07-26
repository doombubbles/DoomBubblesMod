namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessStardustPouch : EndlessPouch<StardustBullet>
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Splits into smaller bullets on hit");
        SacrificeTotal = 1;
    }
}