namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessVortexPouch : EndlessPouch<VortexBullet>
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("Creates bullet echos on enemy hits");
        Item.ResearchUnlockCount = 1;
    }
}