using DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

namespace DoomBubblesMod.Content.Items.Ammo.AllTheBullets;

public class StarWrathBulletPouch : AllTheBulletsPouch<StarWrathBullet>
{
    protected override short SourceItem => ItemID.StarWrath;
}