using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod
{
    class DoomBubblesWorld : ModWorld
    {
        public static double enemyHP;

        public static double bossHP;

        public static double mobDMG;

        public override void Initialize()
        {
            enemyHP = 1.0;
            bossHP = 1.0;
            mobDMG = 1.0;
        }

        public override TagCompound Save()
        {
            return new TagCompound {
                {"enemyHP", enemyHP},
                {"bossHP", bossHP},
                {"mobDMG", mobDMG }
            };
        }

        public override void Load(TagCompound tag)
        {
            enemyHP = tag.GetDouble("enemyHP");
            bossHP = tag.GetDouble("bossHP");
            mobDMG = tag.GetDouble("mobDMG");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                enemyHP = reader.ReadDouble();
                bossHP = reader.ReadDouble();
                mobDMG = reader.ReadDouble();
            }
            else
            {
                ErrorLogger.Log("DoomBubblesMod: Unknown loadVersion: " + loadVersion);
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(enemyHP);
            writer.Write(bossHP);
            writer.Write(mobDMG);
        }

        public override void NetReceive(BinaryReader reader)
        {
            enemyHP = reader.ReadDouble();
            bossHP = reader.ReadDouble();
            mobDMG = reader.ReadDouble();
        }
    }
}
