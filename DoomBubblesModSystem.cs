using System.Collections.Generic;
using DoomBubblesMod.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DoomBubblesMod
{
    public class DoomBubblesModSystem : ModSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "DoomBubblesMod: Infinity Gauntlet",
                    () =>
                    {
                        if (InfinityGauntletUI.visible)
                        {
                            var doomBubblesMod = ModContent.GetInstance<DoomBubblesMod>();
                            doomBubblesMod.infinityGauntletUserInterface.Update(Main
                                ._drawInterfaceGameTime); //I don't understand
                            doomBubblesMod.infinityGauntletUi.Draw(Main.spriteBatch);
                        }

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }
    }
}