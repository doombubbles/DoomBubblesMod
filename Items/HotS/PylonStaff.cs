using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class PylonStaff : TalentItem
    {
        public override bool CloneNewInstances => true;
        public override string Talent1Name => "TalentPylonOvercharge";
        public override string Talent2Name => "TalentConstructAdditionalPylons";
        public override string Talent3Name => "TalentPowerOverflowing";
        protected override Color? TalentColor => Color.Blue;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon Staff");
            Tooltip.SetDefault("Warps in Pylons that power Photon Cannons and give regen\n" +
                               "Max 2");
            
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.mana = 10;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.damage = 96;
            item.shoot = mod.ProjectileType("Pylon");
            item.value = Item.buyPrice(0, 69, 0, 0);
            item.rare = ItemRarityID.Yellow;
        }
        
        public override bool AltFunctionUse(Player player) {
            return ChosenTalent == 1 || ChosenTalent == -1;
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
                Main.PlaySound(SoundLoader.customSoundType, (int) (Main.mouseX + Main.screenPosition.X), (int)
                    ( num145 * 16 - 12), mod.GetSoundSlot(SoundType.Custom, "Sounds/PylonWarpIn"));
                int pylon = Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, num145 * 16 - 12, 0f, 15f, type, damage, knockBack, player.whoAmI, ChosenTalent);
                player.GetModPlayer<DoomBubblesPlayer>().pylons.Add(pylon);
                int maxPylons = ChosenTalent == 2 || ChosenTalent == -1 ? 3 : 2;
                while (player.GetModPlayer<DoomBubblesPlayer>().pylons.Count > maxPylons)
                {
                    player.GetModPlayer<DoomBubblesPlayer>().pylons.RemoveAt(0);
                }
            }
            return false;
        }

        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            
            for (var i = 0; i < tooltips.Count; i++)
            {
                TooltipLine line = tooltips[i];
                if (line.Name == "Damage" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                } else if (line.Name == "Knockback" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                } else if (line.Name == "CritChance" && ChosenTalent != 1 && ChosenTalent != -1)
                {
                    tooltips.RemoveAt(i);
                    i--;
                }
            }
        }
        
    }
}