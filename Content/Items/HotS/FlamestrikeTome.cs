using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Projectiles.HotS;
using DoomBubblesMod.Utils;
using Terraria.DataStructures;

namespace DoomBubblesMod.Content.Items.HotS;

public class FlamestrikeTome : ModItemWithTalents<TalentConvection, TalentIgnite, TalentFuryOfTheSunwell>
{
    protected override Color? TalentColor => Color.LimeGreen;

    public int Verdant => Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<HotSPlayer>().superVerdant
        ? 2
        : Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<HotSPlayer>().verdant
            ? 1
            : 0;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Flamestrike Tome");
        Tooltip.SetDefault("After a delay, deal damage in an area");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 30;
        Item.damage = 115;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Magic;
        Item.useTime = 17;
        Item.useAnimation = 17;
        Item.mana = 17;
        Item.shoot = ProjectileType<FlameStrike>();
        Item.value = Item.buyPrice(0, 69);
        Item.shootSpeed = 1f;
        Item.knockBack = 3f;
        Item.rare = ItemRarityID.Lime;
        Item.autoReuse = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        position.X = Main.MouseWorld.X;
        position.Y = Main.MouseWorld.Y;
        var proj = Projectile.NewProjectile(source, position, new Vector2(0, 0), type, damage, knockback, player.whoAmI,
            ChosenTalent, Verdant);
        Main.projectile[proj].netUpdate = true;
        return false;
    }

    public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
        if (ChosenTalent is 1 or -1)
        {
            damage.Base += player.GetModPlayer<HotSPlayer>().convection;
        }
    }
}