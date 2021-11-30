using DoomBubblesMod.Items.Talent;
using DoomBubblesMod.Projectiles.HotS;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class FlamestrikeTome : ModItemWithTalents<TalentConvection, TalentIgnite, TalentFuryOfTheSunwell>
    {
        protected override Color? TalentColor => Color.LimeGreen;

        public int Verdant => Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<HotSPlayer>().superVerdant ? 2 :
            Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<HotSPlayer>().verdant ? 1 : 0;

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
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.mana = 17;
            Item.shoot = ModContent.ProjectileType<FlameStrike>();
            Item.value = Item.buyPrice(0, 69);
            Item.shootSpeed = 1f;
            Item.knockBack = 3f;
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            position.X = Main.MouseWorld.X;
            position.Y = Main.MouseWorld.Y;
            var proj = Projectile.NewProjectile(source, position, new Vector2(0, 0), type, damage, knockback, player.whoAmI,
                ChosenTalent, Verdant);
            Main.projectile[proj].netUpdate = true;
            return false;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            if (ChosenTalent is 1 or -1)
            {
                flat += player.GetModPlayer<HotSPlayer>().convection;
            }
        }
    }
}