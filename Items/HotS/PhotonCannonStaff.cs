using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class PhotonCannonStaff : TalentItem
    {
        public override string Talent1Name => "TalentWarpResonance";
        public override string Talent2Name => "TalentTowerDefense";
        public override string Talent3Name => "TalentShootEmUp";
        protected override Color? TalentColor => Color.Blue;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photon Cannon Staff");
            Tooltip.SetDefault("Warps Photon Cannons as stationary minions\n" +
                               "Photon Cannons require a Pylon power field");
            
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.mana = 10;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.damage = 96;
            item.shoot = mod.ProjectileType("PhotonCannon");
            item.value = Item.buyPrice(0, 69, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.buffType = mod.BuffType("PhotonCannon");
            item.buffTime = 3600; 
        }
        
        public override bool AltFunctionUse(Player player) {
            return true;
        }
        
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2) {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (player == Main.LocalPlayer && player.altFunctionUse != 2)
            {
                int num144 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
                int num145 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
                if (player.gravDir == -1f)
                {
                    num145 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
                }for (; num145 < Main.maxTilesY - 10 && Main.tile[num144, num145] != null && !WorldGen.SolidTile2(num144, num145) && Main.tile[num144 - 1, num145] != null && !WorldGen.SolidTile2(num144 - 1, num145) && Main.tile[num144 + 1, num145] != null && !WorldGen.SolidTile2(num144 + 1, num145); num145++)
                {
                }
                num145--;
                Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, num145 * 16, 0f, 15f, type, damage, knockBack, player.whoAmI, ChosenTalent);
            }
            return false;
        }
        
        
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(line => line.Name == "BuffTime");
            base.ModifyTooltips(tooltips);
        }
        
        
    }
}