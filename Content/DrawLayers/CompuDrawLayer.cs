using Terraria.DataStructures;
using ReLogic.Content;
using DoomBubblesMod.Content.Items.Armor;

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
        if (player.sleeping.isSleeping) return;

        var color = Color.Cyan;
        if (player.ZoneCrimson) {
            color = Color.Red;
        } else if (player.ZoneCorrupt) {
            color = Color.BlueViolet;
        } else if (player.ZoneUnderworldHeight) {
            color = Color.Orange;
        } else if (player.ZoneHallow) {
            color = Color.HotPink;
        }

        compuproHeadTexture ??= Request<Texture2D>("DoomBubblesMod/Assets/Textures/CompuproEyes", AssetRequestMode.ImmediateLoad);
        var drawX = (player.width / 2) - (player.bodyFrame.Width / 2) + drawInfo.Position.X - Main.screenPosition.X;
        var drawY = 4 + player.height - player.bodyFrame.Height + drawInfo.Position.Y - Main.screenPosition.Y;
        var drawPosition = new Vector2((int) drawX, (int) drawY) + player.headPosition + drawInfo.headVect + drawInfo.helmetOffset;

        var drawData = new DrawData(compuproHeadTexture.Value, drawPosition, player.bodyFrame, color, player.headRotation, drawInfo.headVect,
                1f, drawInfo.playerEffect, 0);
        drawInfo.DrawDataCache.Add(drawData);
    }

    public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
        return drawInfo.drawPlayer.head == CompuproHead.headSlot;
    }
}