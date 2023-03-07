using System;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

public class HorsemansBullet : AllTheBullet
{
    protected override short SourceItem => ItemID.TheHorsemansBlade;

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        if (Main.myPlayer == Projectile.owner &&
            Main.rand.NextFloat() <= .25 &&
            (target.value > 0 || !target.friendly && target.damage > 0))
        {
            HorsemansBlade_SpawnPumpkin(target.whoAmI, Projectile.damage, Projectile.knockBack);
        }
    }

    private void HorsemansBlade_SpawnPumpkin(int npcIndex, int dmg, float kb)
    {
        var player = Projectile.Owner();
        var center = Main.npc[npcIndex].Center;
        var logicCheckScreenHeight = Main.LogicCheckScreenHeight;
        var logicCheckScreenWidth = Main.LogicCheckScreenWidth;
        var num = Main.rand.Next(100, 300);
        var num2 = Main.rand.Next(100, 300);
        num = !Main.rand.NextBool(2)
            ? num + (logicCheckScreenWidth / 2 - num)
            : num - (logicCheckScreenWidth / 2 + num);
        num2 = !Main.rand.NextBool(2)
            ? num2 + (logicCheckScreenHeight / 2 - num2)
            : num2 - (logicCheckScreenHeight / 2 + num2);
        num += (int) player.position.X;
        num2 += (int) player.position.Y;
        var vector = new Vector2(num, num2);
        var num3 = center.X - vector.X;
        var num4 = center.Y - vector.Y;
        var num5 = (float) Math.Sqrt(num3 * num3 + num4 * num4);
        num5 = 8f / num5;
        num3 *= num5;
        num4 *= num5;
        var p = Projectile.NewProjectile(new EntitySource_Parent(Projectile), num, num2, num3, num4,
            ProjectileID.FlamingJack, dmg, kb, player.whoAmI, npcIndex);
        var proj = Main.projectile[p];
        proj.DamageType = Projectile.DamageType;
    }
}