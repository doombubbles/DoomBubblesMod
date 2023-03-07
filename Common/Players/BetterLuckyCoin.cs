using DoomBubblesMod.Common.Configs;

namespace DoomBubblesMod.Common.Players;

public class BetterLuckyCoin : ModPlayer
{
    public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
    {
        LuckyCoin(target, damage);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
    {
        LuckyCoin(target, damage);
    }

    private void LuckyCoin(NPC target, int damage)
    {
        if (GetInstance<ServerConfig>().ProportionateLuckyCoin &&
            damage > 20 &&
            target.value > 0f &&
            Player.hasLuckyCoin &&
            Main.rand.Next(damage) > 20)
        {
            var coinId = ItemID.CopperCoin;
            if (Main.rand.NextBool(10)) coinId = ItemID.SilverCoin;
            if (Main.rand.NextBool(100)) coinId = ItemID.GoldCoin;
            var amount = 1 + Main.rand.Next(5 + damage / 20);
            CoinDrop(target, coinId, amount);
        }
    }

    private void CoinDrop(NPC npc, int coinId, int amount)
    {
        var item = Item.NewItem(new EntitySource_OnHit(Player, npc), npc.position, npc.Size, coinId);
        Main.item[item].stack = amount;
        Main.item[item].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
        Main.item[item].velocity.X = Main.rand.Next(10, 31) * 0.2f * Player.direction;
        Main.item[item].timeLeftInWhichTheItemCannotBeTakenByEnemies = 60;
        if (Main.netMode == NetmodeID.MultiplayerClient)
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
    }
}