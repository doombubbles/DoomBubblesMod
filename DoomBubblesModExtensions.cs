using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    /// <summary>
    /// This class is here until we switch to just requiring the ThoriumMod DLL straight up
    /// </summary>
    public static class DoomBubblesModExtensions
    {
        public static FieldInfo symphonicDamageField;
        public static FieldInfo radiantDamageField;
        public static FieldInfo symphonicCritField;
        public static FieldInfo radiantCritField;
        public static FieldInfo attackSpeedField;

        public static Dictionary<string, FieldInfo> fields;
        
        public static void SymphonicDamage(this Player player, Func<float, float> func)
        {
            player.SetThoriumProperty("symphonicDamage", func);
            
            
            if (DoomBubblesMod.thoriumMod == null) return;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");
            if (symphonicDamageField == null)
            {
                symphonicDamageField = thoriumPlayer.GetType().GetField("symphonicDamage");
            }

            var symphonicDamage = (float) symphonicDamageField.GetValue(thoriumPlayer);
            symphonicDamage = func.Invoke(symphonicDamage);
            symphonicDamageField.SetValue(thoriumPlayer, symphonicDamage);
        }

        public static void RadiantDamage(this Player player, Func<float, float> func)
        {
            if (DoomBubblesMod.thoriumMod == null) return;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");
            if (radiantDamageField == null)
            {
                radiantDamageField = thoriumPlayer.GetType().GetField("radiantBoost");
            }

            if (radiantDamageField != null)
            {

                var radiantDamage = (float) radiantDamageField.GetValue(thoriumPlayer);
                radiantDamage = func.Invoke(radiantDamage);
                radiantDamageField.SetValue(thoriumPlayer, radiantDamage);
            }
        }
        
        public static void SymphonicCrit(this Player player, Func<int, int> func)
        {
            if (DoomBubblesMod.thoriumMod == null) return;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");
            if (symphonicCritField == null)
            {
                symphonicCritField = thoriumPlayer.GetType().GetField("symphonicCrit");
            }

            var symphonicCrit = (int) symphonicCritField.GetValue(thoriumPlayer);
            symphonicCrit = func.Invoke(symphonicCrit);
            symphonicCritField.SetValue(thoriumPlayer, symphonicCrit);
        }

        public static void RadiantCrit(this Player player, Func<int, int> func)
        {
            if (DoomBubblesMod.thoriumMod == null) return;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");
            if (radiantCritField == null)
            {
                radiantCritField = thoriumPlayer.GetType().GetField("radiantCrit");
            }

            var radiantCrit = (int) radiantCritField.GetValue(thoriumPlayer);
            radiantCrit = func.Invoke(radiantCrit);
            radiantCritField.SetValue(thoriumPlayer, radiantCrit);
        }
        
        
        public static void AttackSpeed(this Player player, Func<float, float> func)
        {
            var gottaGoFast = ModLoader.GetMod("GottaGoFast");
            if (gottaGoFast == null)
            {
                var meleeSpeed = player.meleeSpeed;
                meleeSpeed = func.Invoke(meleeSpeed);
                player.meleeSpeed = meleeSpeed;
                return;
            }
            var gottaGoFastPlayer = player.GetModPlayer(gottaGoFast, "GottaGoFastPlayer");
            if (attackSpeedField == null)
            {
                attackSpeedField = gottaGoFastPlayer.GetType().GetField("attackSpeed");
            }
            var attackSpeed = (float) attackSpeedField.GetValue(gottaGoFastPlayer);
            attackSpeed = func.Invoke(attackSpeed);
            attackSpeedField.SetValue(gottaGoFastPlayer, attackSpeed);
        }
        
        public static void AllCrit(this Player player, Func<int, int> func)
        {
            player.meleeCrit = func.Invoke(player.meleeCrit);
            player.rangedCrit = func.Invoke(player.rangedCrit);
            player.magicCrit = func.Invoke(player.magicCrit);
            player.thrownCrit = func.Invoke(player.thrownCrit);
            player.RadiantCrit(func);
            player.SymphonicCrit(func);
        }

        public static void SetThoriumProperty<T>(this Player player, string name, Func<T, T> func)
        {
            if (DoomBubblesMod.thoriumMod == null) return;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");

            var fieldInfo = fields.ContainsKey(name) ? fields[name] : thoriumPlayer.GetType().GetField(name);
            if (fieldInfo == null) return;
            fields[name] = fieldInfo;

            var value = (T)fieldInfo.GetValue(thoriumPlayer);
            value = func.Invoke(value);
            fieldInfo.SetValue(thoriumPlayer, value);
        }
        
        public static T GetThoriumProperty<T>(this Player player, string name)
        {
            if (DoomBubblesMod.thoriumMod == null) return default;
            var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.thoriumMod, "ThoriumPlayer");

            var fieldInfo = fields.ContainsKey(name) ? fields[name] : thoriumPlayer.GetType().GetField(name);
            if (fieldInfo == null) return default;
            fields[name] = fieldInfo;

            return (T)fieldInfo.GetValue(thoriumPlayer);
        }
    }
}