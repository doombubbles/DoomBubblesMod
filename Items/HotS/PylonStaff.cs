using System.Collections.Generic;
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
    public class PylonStaff : ModItemWithTalents<TalentPylonOvercharge, TalentConstructAdditionalPylons, TalentPowerOverflowing>
    {
        protected override Color? TalentColor => Color.Blue;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Staff");
            Tooltip.SetDefault("Warps in Pylons that power Photon Cannons and give regen\n" +
                               "Max 2");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.mana = 10;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.damage = 96;
            Item.shoot = ModContent.ProjectileType<Pylon>();
            Item.value = Item.buyPrice(0, 69);
            Item.rare = ItemRarityID.Yellow;
        }

        public override bool AltFunctionUse(Player player)
        {
            return ChosenTalent == 1 || ChosenTalent == -1;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }

            return base.UseItem(player);
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
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
                    num145 < Main.maxTilesY - 10 && Main.tile[num144, num145] != null &&
                    !WorldGen.SolidTile2(num144, num145) && Main.tile[num144 - 1, num145] != null &&
                    !WorldGen.SolidTile2(num144 - 1, num145) && Main.tile[num144 + 1, num145] != null &&
                    !WorldGen.SolidTile2(num144 + 1, num145);
                    num145++)
                {
                }

                num145--;
                var pylon = Projectile.NewProjectile(source, Main.mouseX + Main.screenPosition.X, num145 * 16 - 12, 0f, 15f,
                    type, damage, knockback, player.whoAmI, ChosenTalent);
                Main.projectile[pylon].netUpdate = true;
                player.GetModPlayer<HotSPlayer>().pylons.Add(pylon);
                var maxPylons = ChosenTalent == 2 || ChosenTalent == -1 ? 3 : 2;
                while (player.GetModPlayer<HotSPlayer>().pylons.Count > maxPylons)
                {
                    player.GetModPlayer<HotSPlayer>().pylons.RemoveAt(0);
                }
            }

            return false;
        }


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);

            for (var i = 0; i < tooltips.Count; i++)
            {
                var line = tooltips[i];
                if (line.Name == "Damage" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                }
                else if (line.Name == "Knockback" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                }
                else if (line.Name == "CritChance" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}