﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DoomBubblesMod.Items.HotS
{
    public class ShieldCapacitor : TalentItem
    {
        public override string Talent1Name => "TalentUnconqueredSpirit";
        public override string Talent2Name => "TalentDampeningField";
        public override string Talent3Name => "TalentPhotonicWeaponry";
        protected override Color? TalentColor => Color.Orange;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield Capacitor");
            Tooltip.SetDefault("You quickly and constantly generate a 50 life shield.");
        }

        public override void SetDefaults()
        {
            item.width = 0;
            item.height = 0;
            item.accessory = true;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.buyPrice(0, 69);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HotSPlayer>().shieldCapacitorChosenTalent = ChosenTalent;
            player.SetThoriumProperty<int>("metalShieldMax", i => i + 100);
            player.GetModPlayer<HotSPlayer>().newShieldCapactior = true;
        }
    }
}