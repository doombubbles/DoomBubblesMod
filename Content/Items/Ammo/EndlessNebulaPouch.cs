namespace DoomBubblesMod.Content.Items.Ammo;

public class EndlessNebulaPouch : EndlessPouch<NebulaBullet>
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Teleports to enemies if close");
        SacrificeTotal = 1;
    }
}