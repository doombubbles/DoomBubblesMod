using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class LivingBombWand : TalentItem
    {
        public override string Talent1Name => "TalentPyromaniac";
        public override string Talent2Name => "TalentSunKingsFury";
        public override string Talent3Name => "TalentMasterOfFlame";
        protected override Color? TalentColor => Color.LimeGreen;

        public int Verdant => Main.player[item.owner].GetModPlayer<HotSPlayer>().superVerdant ? 2 :
            Main.player[item.owner].GetModPlayer<HotSPlayer>().verdant ? 1 : 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Bomb Wand");
            Tooltip.SetDefault("Shoots fireballs that turn targets into Living Bombs");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.value = Item.buyPrice(0, 69);
            item.shoot = mod.ProjectileType("LivingFireball");
            item.damage = 80;
            item.knockBack = 6;
            item.useStyle = 5;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/LivingBombWand"); //SoundID.Item73;
            item.autoReuse = true;
            item.noMelee = true;
            item.magic = true;
            item.useTime = 30;
            item.useAnimation = 30;
            item.mana = 15;
            item.shootSpeed = 7f;
            Item.staff[item.type] = true;
            item.rare = ItemRarityID.Lime;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage,
            ref float knockBack)
        {
            var proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack,
                player.whoAmI, ChosenTalent, Verdant);
            Main.projectile[proj].netUpdate = true;
            return false;
        }
    }
}