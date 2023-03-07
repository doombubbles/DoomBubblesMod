using DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

namespace DoomBubblesMod.Content.Items.Ammo.AllTheBullets;

public class MeowmereBulletPouch : AllTheBulletsPouch<MeowmereBullet>
{
    protected override short SourceItem => ItemID.Meowmere;
}