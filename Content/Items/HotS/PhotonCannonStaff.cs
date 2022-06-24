using System.Collections.Generic;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Projectiles.HotS;

namespace DoomBubblesMod.Content.Items.HotS;

public class PhotonCannonStaff : ModItemWithTalents<TalentWarpResonance, TalentTowerDefense, TalentShootEmUp>
{
    protected override Color? TalentColor => Color.Blue;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Photon Cannon Staff");
        Tooltip.SetDefault("Warps Photon Cannons as stationary minions\n" +
                           "Photon Cannons require a Pylon power field");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;
        Item.mana = 10;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.damage = 96;
        Item.shoot = ProjectileType<PhotonCannon>();
        Item.value = Item.buyPrice(0, 69);
        Item.rare = ItemRarityID.Yellow;
        Item.buffType = BuffType<Buffs.PhotonCannon>();
        Item.buffTime = 3600;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }


    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            player.MinionNPCTargetAim(false);
        }

        return base.UseItem(player);
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        if (player == Main.LocalPlayer && player.altFunctionUse != 2)
        {
            var num144 = (int) (Main.mouseX + Main.screenPosition.X) / 16;
            var num145 = (int) (Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir is -1f)
            {
                num145 = (int) (Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }

            for (;
                 num145 < Main.maxTilesY - 10 &&
                 Main.tile[num144, num145] != null &&
                 !WorldGen.SolidTile2(num144, num145) &&
                 Main.tile[num144 - 1, num145] != null &&
                 !WorldGen.SolidTile2(num144 - 1, num145) &&
                 Main.tile[num144 + 1, num145] != null &&
                 !WorldGen.SolidTile2(num144 + 1, num145);
                 num145++)
            {
            }

            num145--;
            Projectile.NewProjectile(source, Main.mouseX + Main.screenPosition.X, num145 * 16, 0f, 15f, type, damage,
                knockback, player.whoAmI, ChosenTalent);
        }

        return false;
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.RemoveAll(line => line.Name == "BuffTime");
        base.ModifyTooltips(tooltips);
    }
}