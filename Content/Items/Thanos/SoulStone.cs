using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Thanos;

internal class SoulStone : InfinityStone
{
    protected override int Rarity => ItemRarityID.Quest;
    protected override int Gem => ItemID.Amber;
    protected override Color Color => InfinityGauntlet.SoulColor;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Soul Stone");
        Tooltip.SetDefault("\"Soul holds a special place among the Infinity Stones.\n" +
                           "You might say, it is a certain wisdom. To ensure that\n" +
                           "whoever possesses it understands its power, the stone\n" +
                           "demands a sacrifice. In order to take the stone, you\n" +
                           "must lose that which you love. A soul for a soul.\"\n" +
                           "-The Stonekeeper");
        Item.SetResearchAmount(1);
    }
    
    public static void SoulAbility(Player player)
    {
        if (player.chest == -3)
        {
            player.chest = -1;
            Recipe.FindRecipes();
        }
        else
        {
            player.chest = -3;
            player.chestX = (int) (player.position.X / 16f);
            player.chestY = (int) (player.position.Y / 16f);
            player.SetTalkNPC(-1);
            Main.SetNPCShopIndex(0);
            Main.playerInventory = true;
            Main.recBigList = false;
            Recipe.FindRecipes();
            player.GetModPlayer<ThanosPlayer>().soulStone = true;
        }
    }
}