using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class VerdantSpheres : TalentItem
    {
        public override string Talent1Name => "TalentFelInfusion";
        public override string Talent2Name => "TalentManaTap";
        public override string Talent3Name => "TalentTwinSpheres";
        protected override Color? TalentColor => Color.Lime;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verdant Spheres");
            Tooltip.SetDefault("Flamestrike has increased radius and damage\n" +
                               "Living Bomb has extra pierce and costs no mana");
        }

        public override void SetDefaults()
        {
            item.width = 0;
            item.height = 0;
            item.accessory = true;
            item.rare = ItemRarityID.Lime;
            item.value = Item.buyPrice(0, 69);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HotSPlayer>().verdant = true;
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                player.allDamage -= .1f;
                player.magicDamage += .25f;
            }

            if (ChosenTalent == 2 || ChosenTalent == -1)
            {
                player.GetModPlayer<HotSPlayer>().manaTap = true;
            }

            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                player.GetModPlayer<HotSPlayer>().superVerdant = true;
            }
            player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(mod.ItemType("LivingBombWand"));
        }
    }
}