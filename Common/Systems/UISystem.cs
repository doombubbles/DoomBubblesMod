using System.Collections.Generic;
using Terraria.UI;

namespace DoomBubblesMod.Common.Systems;

public class UISystem : ModSystem
{
    public UserInterface InfinityGauntlet { get; private set; }

    public override void Load()
    {
        if (!Main.dedServ)
        {
            InfinityGauntlet = new UserInterface();
            // InfinityGauntlet.SetState(new InfinityGauntletUI());
        }
    }

    public override void Unload()
    {
        InfinityGauntlet = null;
    }

    public override void UpdateUI(GameTime gameTime)
    {
        InfinityGauntlet.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "DoomBubblesMod: Infinity Gauntlet",
                () =>
                {
                    InfinityGauntlet?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}