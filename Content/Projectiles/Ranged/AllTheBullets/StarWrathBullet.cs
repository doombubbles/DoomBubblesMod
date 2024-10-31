using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Projectiles.Ranged.AllTheBullets;

public class StarWrathBullet : AllTheBullet
{
    protected override short SourceItem => ItemID.StarWrath;

    public override bool PreKill(int timeLeft)
    {
        if (Main.myPlayer == Projectile.owner &&
            Main.rand.NextFloat() <= .5)
        {
            DoStarWrath();
        }

        return true;
    }

    // pointPoisition = new Vector2(position.X + (float)width * 0.5f + (float)(Main.rand.Next(201) * -direction) + ((float)Main.mouseX + Main.screenPosition.X - position.X), MountedCenter.Y - 600f);
    
    private void DoStarWrath()
    {
        var value4 = Projectile.Center;
        var player = Projectile.Owner();
        var num42 = value4.Y;
        if (num42 > player.Center.Y - 200f)
            num42 = player.Center.Y - 200f;

        var pointPosition = player.Center + new Vector2(-Main.rand.Next(0, 401) * player.direction, -600f);
        var vector14 = value4 - pointPosition;
        if (vector14.Y < 0f)
            vector14.Y *= -1f;

        if (vector14.Y < 20f)
            vector14.Y = 20f;

        vector14.Normalize();
        vector14 *= Projectile.oldVelocity.Length();
        var num2 = vector14.X;
        var num3 = vector14.Y;
        var speedX6 = num2;
        var speedY8 = num3 + Main.rand.Next(-40, 41) * 0.02f;
        var p = Projectile.NewProjectile(new EntitySource_Parent(Projectile), pointPosition.X, pointPosition.Y,
            speedX6, speedY8, ProjectileID.StarWrath, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f,
            num42);
        var proj = Main.projectile[p];
        proj.DamageType = Projectile.DamageType;
    }
}