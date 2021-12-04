using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class MartianMeteorHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Martian Meteor Helmet");
        Tooltip.SetDefault("17% Increased Magic Damage");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        var realSlot = Item.headSlot;
        Item.CloneDefaults(ItemID.MeteorHelmet);
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(0, 22, 50);
        Item.defense = 15;
        Item.headSlot = realSlot;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<MartianMeteorSuit>() &&
               legs.type == ModContent.ItemType<MartianMeteorLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Laser Machinegun costs 0 mana";
        player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(ItemID.LaserMachinegun);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .17f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ModContent.ItemType<LaserMeteorHelmet>());
        recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}