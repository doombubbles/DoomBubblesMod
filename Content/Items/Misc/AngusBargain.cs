using System;
using System.Linq;
using Terraria.Audio;
using Terraria.Chat;

namespace DoomBubblesMod.Content.Items.Misc;

public class AngusBargain : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Angus's Bargain");
        // Tooltip.SetDefault("Gives 100k Gold on use");
        Item.ResearchUnlockCount = 1;

        NPCID.Sets.MPAllowedEnemies[NPCID.DD2Betsy] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 36;
        Item.maxStack = 20;
        Item.value = 0;
        Item.rare = ItemRarityID.Yellow;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = true;
    }

    public override bool CanUseItem(Player player)
    {
        return !Main.npc.Any(npc => npc.active && npc.type == NPCID.DD2Betsy);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.LivingFireBlock, 25)
        .AddIngredient(ItemID.SoulofFlight, 5)
        .AddIngredient(ItemID.BeetleHusk, 3)
        .AddTile(TileID.MythrilAnvil)
        .Register();

    public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
    {
        if (CanUseItem(player) && player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            var message = $"Whoops, {player.name} wasn't B-Hopping";
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // If the player is not in multiplayer, spawn directly
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.DD2Betsy);
                Main.NewText(message);
            }
            else
            {
                // If the player is in multiplayer, request a spawn
                // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in this class above
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: NPCID.DD2Betsy);
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), Color.White);
            }

            return true;
        }

        return base.UseItem(player);
    }
}