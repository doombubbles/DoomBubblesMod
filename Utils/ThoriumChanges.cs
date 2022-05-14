using System.Linq;
using log4net;
using MonoMod.Cil;
using static Mono.Cecil.Cil.OpCodes;

// using ThoriumMod.Items;

namespace DoomBubblesMod.Utils;

public static class ThoriumChanges
{
    private static ILog Logger => GetInstance<DoomBubblesMod>().Logger;

    // private Dictionary<MethodInfo, Action<ILContext>> thoriumILChanges;

    internal static void Load()
    {
        //thoriumILChanges = new Dictionary<MethodInfo, Action<ILContext>>();

        var thoriumMod = DoomBubblesMod.ThoriumMod;
        if (thoriumMod != null)
        {
            /* TODO THORIUM
            IL.ThoriumMod.ThoriumPlayer.PostUpdateEquips += OnModifyThoriumPostUpdateEquips;

            IL.ThoriumMod.Items.BasicAccessories.CopperBuckler.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.TinBuckler.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.IronShield.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.LeadShield.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.SilverBulwark.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.TungstenBulwark.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.GoldAegis.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.BasicAccessories.PlatinumAegis.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.Thorium.ThoriumShield.UpdateAccessory += OnModifyThoriumShield;
            IL.ThoriumMod.Items.NPCItems.AstroBeetleHusk.UpdateAccessory += OnModifyThoriumShield;

            On.ThoriumMod.Items.ThoriumItem.CanEquipAccessory += (orig, self, player, slot) =>
                self.accessoryType == AccessoryType.GemRing || orig(self, player, slot);
                */


            //ModifyThoriumIL("ThoriumPlayer", "PostUpdateEquips", OnModifyThoriumPostUpdateEquips);
            /*ModifyThoriumIL("CopperBuckler", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("TinBuckler", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("IronShield", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("LeadShield", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("SilverBulwark", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("TungstenBulwark", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("GoldAegis", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("PlatinumAegis", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("ThoriumShield", "UpdateAccessory", OnModifyThoriumShield);
            ModifyThoriumIL("AstroBeetleHusk", "UpdateAccessory", OnModifyThoriumShield);*/

            /*ModifyThoriumIL("AmberRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("AmethystRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("DiamondRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("EmeraldRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("OnyxRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("OpalRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("PearlRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("RubyRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("SapphireRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("TopazRing", "CanEquipAccessory", OnModifyThoriumRing);
            ModifyThoriumIL("TheRing", "CanEquipAccessory", OnModifyThoriumRing);*/
        }
    }

    internal static void Unload()
    {
        /*foreach (var (methodInfo, action) in thoriumILChanges)
        {
            HookEndpointManager.Unmodify(methodInfo, action);
        }

        thoriumILChanges = null;*/
    }

    private static void OnModifyThoriumRing(ILContext il)
    {
        var c = new ILCursor(il);
        if (!c.TryGotoNext(i => i.OpCode == Ldc_I4_S))
        {
            Logger.Info("BALLZ I couldn't find ldc.i4.s 10");
            return;
        }

        c.Remove();
        c.Emit(Ldc_I4_0);
    }

    private static void OnModifyThoriumShield(ILContext il)
    {
        var c = new ILCursor(il);
        if (!c.TryGotoNext(i => i.MatchStfld("ThoriumMod.ThoriumPlayer", "metalShieldMax")))
        {
            Logger.Info("BALLZ I couldn't find metalShieldMax");
            return;
        }

        var field = c.Next.Operand;

        c.Index--;

        c.Emit(Dup);
        c.Emit(Ldfld, field);
        c.Index++;
        c.Emit(Add);
    }


    private static void OnModifyThoriumPostUpdateEquips(ILContext il)
    {
        var c = new ILCursor(il);
        var test = 0;
        while (true)
        {
            var fieldInfo = DoomBubblesMod.ThoriumMod
                .GetType()
                .Assembly.GetTypes()
                .First(type => type.Name == "ThoriumPlayer")
                .GetField("shieldHealth");
            if (!c.TryGotoNext(i => i.MatchLdfld(fieldInfo)))
            {
                Logger.Info("It couldn't find the shieldHealth 50");
                return;
            }

            ;
            c.Index++;

            if (c.Next.MatchLdcI4(50))
            {
                c.Remove();
                c.Emit(Ldc_I4_S, (sbyte) 100);
                c.Index++;
                c.GotoNext(i => i.OpCode == Ldc_I4_S);
                c.Remove();
                c.Emit(Ldc_I4_S, (sbyte) 100);

                return;
            }

            if (test > 20)
            {
                Logger.Info("It couldn't find the shieldHealth 50 because of weird");
                return;
            }

            test++;
        }
    }


    /*private void ModifyThoriumIL(string className, string methodName, Action<ILContext> method)
    {
        var thoriumMod = DoomBubblesMod.thoriumMod;
        if (thoriumMod != null)
        {
            var thoriumAssembly = thoriumMod.GetType().Assembly;

            var thoriumClass = thoriumAssembly.GetTypes().FirstOrDefault(type => type.Name == className);
            if (thoriumClass != null)
            {
                var thoriumMethod = thoriumClass.GetMethod(methodName);
                if (thoriumMethod != null)
                {
                    HookEndpointManager.Modify(thoriumMethod, method);
                    thoriumILChanges[thoriumMethod] = method;
                }
                else
                {
                    Logger.Info($"BALLZ I couldn't find {className}.{methodName}");
                }
            }
            else
            {
                Logger.Info($"BALLZ I couldn't find {className}");
            }
        }
    }*/


    internal static void ModifyThoriumRecipes()
    {
        var thoriumMod = DoomBubblesMod.ThoriumMod;
        if (thoriumMod == null)
        {
            return;
        }

        Recipe recipe;

        for (var i = 0; i < Recipe.numRecipes; i++)
        {
            recipe = Main.recipe[i];

            if (recipe.HasResult(thoriumMod, "TerraStaff") ||
                recipe.HasResult(thoriumMod, "TerraStaff") ||
                recipe.HasResult(thoriumMod, "TerraStaff") ||
                recipe.HasResult(thoriumMod, "TerraStaff"))
            {
                //recipe.AddIngredient(ModContent.ItemType<HeartOfTerraria>());
            }
        }
    }
}