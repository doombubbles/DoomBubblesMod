using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Items.HotS
{
    public class LightningSurge : TalentItem
    {
        public override string Talent1Name => "TalentSustainingPower";
        public override string Talent2Name => "TalentLightningBarrage";
        public override string Talent3Name => "TalentNegativelyCharged";
        protected override Color? TalentColor => Color.Red;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Surge");
            Tooltip.SetDefault("Shoots lightning at an enemy by your cursor\n" +
                               "Enemies hit along the way take bonus damage");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 36;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.damage = 62;
            item.shoot = mod.ProjectileType("AlarakLightning");
            item.useAnimation = 25;
            item.useTime = 25;
            item.magic = true;
            item.knockBack = 3;
            item.useStyle = 5;
            item.rare = 10;
            item.mana = 12;
            item.autoReuse = false;
            item.value = Item.buyPrice(0, 69, 0, 0);
        }

        public override bool CanUseItem(Player player)
        {
            return FindNpcs(player) != null;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            var npcs = FindNpcs(player);
            var npc = npcs[0].Key;
            if (npc != -1)
            {
                NPC target = Main.npc[npc];
                Main.PlaySound(SoundLoader.customSoundType, (int)position.X, (int)position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/LightningSurge2"));
                 Projectile.NewProjectile(position, new Vector2((target.Center.X - position.X) / 200f, (target.Center.Y - position.Y) / 200f), type, damage, knockBack, player.whoAmI, npc, ChosenTalent);
            }

            if ((ChosenTalent == 2 || ChosenTalent == -1) && npcs.Count > 1)
            {
                NPC target = Main.npc[npcs[1].Key];
                Main.PlaySound(SoundLoader.customSoundType, (int)position.X, (int)position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/LightningSurge2"), 1f, -0.1f);
                Projectile.NewProjectile(position, new Vector2((target.Center.X - position.X) / 200f, (target.Center.Y - position.Y) / 200f), type, damage, knockBack, player.whoAmI, target.whoAmI, ChosenTalent);
            }
            return false;
        }

        private static List<KeyValuePair<int, float>> FindNpcs(Player player)
        {
            //Try to find an enemy by the mouse cursor
            var maxDistance = 75f;
            
            var potentialTargets = new Dictionary<int, float>();
            
            for (var i = 0; i < Main.npc.Length; i++)
            {
                var npc = Main.npc[i];

                var distance = npc.Distance(Main.MouseWorld);
                if (npc.active && !npc.dontTakeDamage && !npc.townNPC && npc.immune[player.whoAmI] == 0 && Main.myPlayer == player.whoAmI && distance <= maxDistance)
                {
                    potentialTargets.Add(npc.whoAmI, distance);
                }
            }
            var list = potentialTargets.ToList();
            if (list.Count == 0)
            {
                return null;
            }
            list.Sort((pair, valuePair) => pair.Value.CompareTo(valuePair.Value));

            return list;
        }
    }
}