using Terraria.DataStructures;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using DoomBubblesMod.Common.Players;

namespace DoomBubblesMod.Content.DrawLayers;

public class CompuDrawLayer : PlayerDrawLayer
{
    private Asset<Texture2D> compuproHeadTexture;

    public override Position GetDefaultPosition()
    {
        return new AfterParent(PlayerDrawLayers.Head);
    }

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        compuproHeadTexture ??= Request<Texture2D>("DoomBubblesMod/Assets/Textures/CompuproEyes", AssetRequestMode.ImmediateLoad);
        var drawX = (player.width / 2) - (player.bodyFrame.Width / 2) + drawInfo.Position.X - Main.screenPosition.X;
        var drawY = 4 + player.height - player.bodyFrame.Height + drawInfo.Position.Y - Main.screenPosition.Y;
        var drawPosition = new Vector2((int) drawX, (int) drawY) + player.headPosition + drawInfo.headVect + drawInfo.helmetOffset;

        var drawData = new DrawData(compuproHeadTexture.Value, drawPosition, player.bodyFrame, Color.White, player.headRotation, drawInfo.headVect,
                1f, drawInfo.playerEffect, 0);
        drawInfo.DrawDataCache.Add(drawData);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        return drawInfo.drawPlayer.head == ModContent.GetModHeadSlot("DoomBubblesMod/Items/Armor/CompuproHead_Head");
    }
}