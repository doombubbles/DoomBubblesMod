using Microsoft.Xna.Framework;
using On.Terraria.ObjectData;
using Terraria;
using Terraria.ID;

namespace DoomBubblesMod.Items.HotS
{
    public class FlamestrikeTome : TalentItem
    {
        public override string Talent1Name => "TalentConvection";
        public override string Talent2Name => "TalentIgnite";
        public override string Talent3Name => "TalentFuryOfTheSunwell";
        protected override Color? TalentColor => Color.LimeGreen;
        
        public int Verdant => Main.player[item.owner].GetModPlayer<HotSPlayer>().superVerdant ? 2 : Main.player[item.owner].GetModPlayer<HotSPlayer>().verdant ? 1 : 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamestrike Tome");
            Tooltip.SetDefault("After a delay, deal damage in an area");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.damage = 115;
            item.useStyle = 5;
            item.noMelee = true;
            item.magic = true;
            item.useTime = 17;
            item.useAnimation = 17;
            item.mana = 17;
            item.shoot = mod.ProjectileType("FlameStrike");
            item.value = Item.buyPrice(0, 69);
            item.shootSpeed = 1f;
            item.knockBack = 3f;
            item.rare = ItemRarityID.Lime;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            position.X = Main.MouseWorld.X;
            position.Y = Main.MouseWorld.Y;
            int proj = Projectile.NewProjectile(position, new Vector2(0, 0), type, damage, knockBack, player.whoAmI, ChosenTalent, Verdant);
            Main.projectile[proj].netUpdate = true;
            return false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                flat += player.GetModPlayer<HotSPlayer>().convection;
            }
        }
    }
}