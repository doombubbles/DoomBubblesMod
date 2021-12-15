﻿using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Projectiles.HotS;
using DoomBubblesMod.Utils;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Items.HotS;

public class RepeaterCannon : ModItemWithTalents<TalentMobileOffense, TalentOffensiveCadence, TalentArsenalOvercharge>
{
    private short mShot;

    protected override Color? TalentColor => Color.Orange;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Repeater Cannon");
        Tooltip.SetDefault("Shots build up to empower your next Phase Bomb\n" +
                           "(Stacks up to 10)");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 74;
        Item.height = 26;
        Item.noMelee = true;
        Item.damage = 83;
        Item.shoot = ModContent.ProjectileType<Repeater>();
        Item.shootSpeed = 15f;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.DamageType = DamageClass.Ranged;
        Item.knockBack = 5;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Red;
        Item.autoReuse = true;
        Item.value = Item.buyPrice(0, 69);
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-10, 3);
    }

    public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
    {
        if (ChosenTalent == 1 || ChosenTalent == -1)
        {
            flat += player.velocity.Length() / 50f;
        }
    }

    public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        mShot++;

        if ((ChosenTalent == 2 || ChosenTalent == -1) && mShot == 3)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<RepeaterBig>(),
                damage * 2, knockback * 2f, player.whoAmI, 4, ChosenTalent);
        }
        else
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI,
                mShot, ChosenTalent);
        }

        if (mShot >= 3)
        {
            mShot = 0;
        }

        return false;
    }

    public override float UseTimeMultiplier(Player player)
    {
        return base.UseTimeMultiplier(player) * (1f + .1f * player.GetModPlayer<HotSPlayer>().fenixRepeaterBuff);
    }
}