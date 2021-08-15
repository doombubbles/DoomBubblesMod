using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Items.Talent;
using DoomBubblesMod.Projectiles.HotS;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

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
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.damage = 62;
            Item.shoot = ModContent.ProjectileType<AlarakLightning>();
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3;
            Item.useStyle = 5;
            Item.rare = 10;
            Item.mana = 12;
            Item.autoReuse = false;
            Item.value = Item.buyPrice(0, 69);
        }

        public override bool CanUseItem(Player player)
        {
            return FindNpcs(player) != null;
        }

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            var npcs = FindNpcs(player);
            if (npcs == null)
            {
                return false;
            }
            var npc = npcs[0].Key;
            if (npc != -1)
            {
                var target = Main.npc[npc];
                var proj = Projectile.NewProjectile(source, position,
                    new Vector2((target.Center.X - position.X) / 200f, (target.Center.Y - position.Y) / 200f), type,
                    damage, knockback, player.whoAmI, npc, ChosenTalent);
                Main.projectile[proj].netUpdate = true;
            }

            if ((ChosenTalent == 2 || ChosenTalent == -1) && npcs.Count > 1)
            {
                for (var i = 1; i < (player.gravControl2 ? npcs.Count : 1); i++)
                {
                    var target = Main.npc[npcs[i].Key];
                    var proj = Projectile.NewProjectile(source, position,
                        new Vector2((target.Center.X - position.X) / 200f, (target.Center.Y - position.Y) / 200f), type,
                        damage, knockback, player.whoAmI, target.whoAmI, ChosenTalent);
                    Main.projectile[proj].netUpdate = true;
                }
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
                if (npc.active && !npc.dontTakeDamage && !npc.townNPC && npc.immune[player.whoAmI] == 0 &&
                    Main.myPlayer == player.whoAmI && distance <= maxDistance)
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