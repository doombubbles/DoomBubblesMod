using DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

namespace DoomBubblesMod.Content.Items.Ammo.AllTheBullets;

public class HorsemansBulletPouch : AllTheBulletsPouch<HorsemansBullet>
{
    protected override short SourceItem => ItemID.TheHorsemansBlade;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("The Horseman's Bullet Pouch");
    }
}