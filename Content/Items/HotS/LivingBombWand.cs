using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Projectiles.HotS;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.HotS;

public class LivingBombWand : ModItemWithTalents<Pyromaniac, SunKingsFury, MasterOfFlame>
{
    protected override Color? TalentColor => Color.LimeGreen;

    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Shoots fireballs that turn targets into Living Bombs");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.value = Item.buyPrice(0, 69);
        Item.shoot = ProjectileType<LivingFireball>();
        Item.damage = 80;
        Item.knockBack = 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = Mod.Sound("LivingBombWand");
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Magic;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.mana = 15;
        Item.shootSpeed = 7f;
        Item.staff[Item.type] = true;
        Item.rare = ItemRarityID.Lime;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        var proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback,
            player.whoAmI, ChosenTalent, player.GetModPlayer<HotsPlayer>().verdant);
        Main.projectile[proj].netUpdate = true;
        return false;
    }
}