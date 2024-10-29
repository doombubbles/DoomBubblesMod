using Terraria;
using Terraria.ID;

namespace DoomBubblesMod.Common.Players;

public class BetterWeaponImbues : ModPlayer
{
    public override void SetStaticDefaults()
    {
        Main.meleeBuff[BuffID.WeaponImbueGold] = false;
        Main.meleeBuff[BuffID.WeaponImbueConfetti] = false;
    }

    public override void PostUpdateBuffs()
    {
        if (Player.HasBuff(BuffID.WeaponImbueVenom))
        {
            Player.meleeEnchant = 1;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueCursedFlames))
        {
            Player.meleeEnchant = 2;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueFire))
        {
            Player.meleeEnchant = 3;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueIchor))
        {
            Player.meleeEnchant = 5;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueNanites))
        {
            Player.meleeEnchant = 6;
        }
        else if (Player.HasBuff(BuffID.WeaponImbuePoison))
        {
            Player.meleeEnchant = 8;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueGold))
        {
            Player.meleeEnchant = 4;
        }
        else if (Player.HasBuff(BuffID.WeaponImbueConfetti))
        {
            Player.meleeEnchant = 7;
        }
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
    {
        if (item.CountsAsClass(DamageClass.Melee))
        {
            Apply(target);
        }
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
    {
        if (proj.DamageType.CountsAsClass(DamageClass.Melee) || ProjectileID.Sets.IsAWhip[proj.type])
        {
            Apply(target);
        }
    }

    public void Apply(NPC target)
    {
        if (Player.HasBuff(BuffID.WeaponImbueGold) && Player.meleeEnchant != 4)
        {
            target.AddBuff(BuffID.Midas, 120);
        }

        if (Player.HasBuff(BuffID.WeaponImbueConfetti) && Player.meleeEnchant != 7)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Player), target.Center, target.velocity,
                ProjectileID.ConfettiMelee, 0, 0f, Player.whoAmI);
        }
    }
}