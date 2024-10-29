using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Accessories.Glove;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Emblem;

public class ChromaticEmblem : ModItem, IHasThoriumRecipe
{
    private const float Factor = .15f;

    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault($"{Factor:P0} increased damage\n" +
                           $"{Factor:P0} increased crit chance\n" +
                           $"{Factor:P0} increased attack speed\n" +
                           $"{Factor:P0} reduced damage taken\n" +
                           $"Increases armor penetration by {Factor * 100:N0}\n" +
                           "Gravity Globe benefits ;)\n" +
                           "Can't be equipped with any other 'Emblems'"); */
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(1);
        Item.width = 28;
        Item.height = 28;
        Item.rare = -12;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += Factor;
        player.GetCritChance(DamageClass.Generic) += Factor * 100;
        player.GetArmorPenetration(DamageClass.Generic) += Factor * 100;
        player.GetAttackSpeed(DamageClass.Generic) += Factor;
        player.endurance += Factor;

        player.gravControl2 = true;
        player.gravControl = true;
        player.GetDoomBubblesPlayer().emblem = -42;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        return player.GetModPlayer<DoomBubblesPlayer>().emblem == 0 && base.CanEquipAccessory(player, slot, modded);
    }

    public void AddThoriumRecipe(Mod thoriumMod)
    {
        var recipe = CreateRecipe();
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.AddIngredient(ItemType<TimsRegret>());
        recipe.AddIngredient(ItemType<ChromaticGauntlet>());
        recipe.AddIngredient(thoriumMod.Find<ModItem>("TheRing"));
        recipe.AddIngredient(ItemType<NebulaEmblem>());
        recipe.AddIngredient(ItemType<SolarEmblem>());
        recipe.AddIngredient(ItemType<StardustEmblem>());
        recipe.AddIngredient(ItemType<VortexEmblem>());
        recipe.AddIngredient(ItemID.GravityGlobe);
        recipe.Register();
    }
}