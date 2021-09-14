using DoomBubblesMod.Items.Talent;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class VerdantSpheres : ModItemWithTalents<TalentFelInfusion, TalentManaTap, TalentTwinSpheres>
    {
        protected override Color? TalentColor => Color.Lime;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verdant Spheres");
            Tooltip.SetDefault("Flamestrike has increased radius and damage\n" +
                               "Living Bomb has extra pierce and costs no mana");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 0;
            Item.height = 0;
            Item.accessory = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.buyPrice(0, 69);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HotSPlayer>().verdant = true;
            if (ChosenTalent is 1 or -1)
            {
                player.GetDamage(DamageClass.Generic) -= .1f;
                player.GetDamage(DamageClass.Magic) += .25f;
            }

            if (ChosenTalent is 2 or -1)
            {
                player.GetModPlayer<HotSPlayer>().manaTap = true;
            }

            if (ChosenTalent is 3 or -1)
            {
                player.GetModPlayer<HotSPlayer>().superVerdant = true;
            }

            player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(ModContent.ItemType<LivingBombWand>());
        }
    }
}