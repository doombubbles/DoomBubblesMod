using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Items
{
	public class DoomBubblesGlobalItem : GlobalItem
	{
		public override void SetDefaults(Item item)
		{
			
			Dictionary<string, string> itemNameOverrides = new Dictionary<string, string>
			{
				{"Avenger Emblem", "Avengers Emblem"},
				{"Celestial Fragment", "Heavenly Fragment"},
				{"Celestial Crown", "Heavenly Crown"},
				{"Celestial Vestment", "Heavenly Vestment"},
				{"Celestial Leggings", "Heavenly Leggings"},
				{"Celestial Carrier", "Heavenly Carrier"},
				{"Celestial Burst Staff", "Heavenly Burst Staff"},
				{"Waifu in a Bottle", "Weeaboo in a Bottle"},
				{"Rare Waifu in a Bottle", "Rare Weeaboo in a Bottle"},
				{"Twin's Ire", "Twin's Aiur"}
			};

			foreach ( KeyValuePair<string, string> kvp in itemNameOverrides)
			{
				string itemId = kvp.Key;
				string replacementName = kvp.Value;
				if (item.Name == itemId)
				{
					item.SetNameOverride(replacementName);
				}
			}
			
			if (item.type == ItemID.SpikyBall)
			{
                item.ammo = item.type;
			}
			
			
			
			if (radiantWeapons.Contains(item.Name))
			{
				item.damage *= 2;
			}
			
		}

		public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor,
			Color itemColor, Vector2 origin, float scale)
		{
			LoLPlayer lolPlayer = Main.player[item.owner].GetModPlayer<LoLPlayer>();
			if (lolPlayer.Predator && lolPlayer.keystoneCooldown == 0 && item.shoeSlot > 0)
			{
				spriteBatch.Draw(mod.GetTexture("UI/Keystones/Predator"), position + new Vector2(-5, 20),
					new Rectangle(2, 2, 26, 26), drawColor, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
			}
		}

		public override bool CanUseItem(Item item, Player player)
		{
			if (player.GetModPlayer<ThanosPlayer>().INfinityGauntlet && (DoomBubblesMod.PowerStoneHotKey.Current))
			{
				return false;
			}
			return base.CanUseItem(item, player);
		}

		public override void GetWeaponCrit(Item item, Player player, ref int crit)
		{
			if (radiantWeapons.Contains(item.Name))
			{
				crit += player.GetModPlayer<DoomBubblesPlayer>().customRadiantCrit;
			}

			if (symphonicWeapons.Contains(item.Name))
			{
				crit += player.GetModPlayer<DoomBubblesPlayer>().customSymphonicCrit;
			}
			
			crit = (int) (crit * player.GetModPlayer<DoomBubblesPlayer>().critChanceMult);
			base.GetWeaponCrit(item, player, ref crit);
		}

		public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
		{
			if ((item.type == ItemID.ExplodingBullet || item.Name == "Endless Explosive Pouch") &&
			    player.GetModPlayer<DoomBubblesPlayer>().explosionBulletBonus)
			{
				mult *= 2;
			}
			
			if (radiantWeapons.Contains(item.Name))
			{
				mult += (player.GetModPlayer<DoomBubblesPlayer>().customRadiantDamage - 1f);
			}
			
			if (symphonicWeapons.Contains(item.Name))
			{
				mult += (player.GetModPlayer<DoomBubblesPlayer>().customSymphonicDamage - 1f);
			}

			base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
		}

		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			if (arg == ItemID.PlanteraBossBag && context == "bossBag")
			{
				player.QuickSpawnItem(mod.ItemType("HeartOfTerraria"));
			}
			if (arg == ItemID.FishronBossBag && context == "bossBag")
			{
				if (Main.rand.Next(1, 5) == 1)
				{
					player.QuickSpawnItem(mod.ItemType("Ultrashark"));
				}
			}
			base.OpenVanillaBag(context, player, arg);
		}

		public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
		{
			if (player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Contains(item.type))
			{
				mult = 0f;
			}
		}

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.type == ItemID.GravityGlobe)
			{
				tooltips.Add(new TooltipLine(mod, "Secret", "Now has certain special properties...")
				{
					overrideColor = Color.MediumPurple
				});
				tooltips.Add(new TooltipLine(mod, "Secret2", "	-doombubbles"){
					overrideColor = Color.MediumPurple
				});
			}
			
			if (item.owner != 255 && item.owner != -1 && Main.player[item.owner].GetModPlayer<DoomBubblesPlayer>().noManaItems.Contains(item.type))
			{
				for (var i = 0; i < tooltips.Count; i++)
				{
					if (tooltips[i].Name == "UseMana" && tooltips[i].mod.Equals("Terraria"))
					{
						tooltips.RemoveAt(i);
						i--;
					}
				}
			}
		}

		public override void HoldItem(Item item, Player player)
		{
			List<int> spellBooks = new List<int>() {ItemID.WaterBolt, ItemID.BookofSkulls, ItemID.DemonScythe, ItemID.CursedFlames, 
				ItemID.GoldenShower, ItemID.CrystalStorm, ItemID.MagnetSphere, ItemID.RazorbladeTyphoon, ItemID.LunarFlareBook, mod.ItemType("FlamestrikeTome")};
			if (player.GetModPlayer<LoLPlayer>().UnsealedSpellbook && spellBooks.Contains(item.type) &&
			    player.GetModPlayer<LoLPlayer>().keystoneCooldown == 0)
			{
				Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
				if (player.direction != 1)
				{
					vector.X = player.bodyFrame.Width - vector.X;
				}
				if (player.gravDir != 1f)
				{
					vector.Y = player.bodyFrame.Height - vector.Y;
				}
				vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
				Vector2 position = player.RotatedRelativePoint(player.position + vector) - player.velocity;
				Color color = Color.LightBlue;
				Dust dust = Main.dust[Dust.NewDust(position, 0, 0, 212, 0, 0, 0, (Color) color, 1.5f)];
				dust.velocity *= .1f;
				dust.velocity += player.velocity;
				dust.noGravity = true;
			}
		}

		public override void UpdateAccessory(Item item, Player player, bool hideVisual)
		{
			if (item.Name.Contains("Emblem") && item.Name != "Aquatic Emblem")
			{
				player.GetModPlayer<DoomBubblesPlayer>().emblem++;
			}
			base.UpdateAccessory(item, player, hideVisual);
		}

		public override bool CanEquipAccessory(Item item, Player player, int slot)
		{
			if (item.Name.Contains("Emblem") && player.GetModPlayer<DoomBubblesPlayer>().emblem == -1)
			{
				return false;
			}
			return base.CanEquipAccessory(item, player, slot);
		}

		#region dumbLongList
		public static List<String> radiantWeapons = new List<String>()
		{
			"Life Quartz Claymore",
			"Heretic Breaker",
			"Light Bringer's Warhammer",
			"Wooden Baton",
			"Ice Shaver",
			"Dark Scythe",
			"Crimson Scythe",
			"Bone Reaper",
			"Bountiful Harvest",
			"Molten Thresher",
			"Aquaite Scythe",
			"Bat Scythe",
			"Falling Twilight",
			"Blood Harvest",
			"Bone Baton",
			"Lustrous Baton",
			"Morning Dew",
			"Titan Scythe",
			"Hallowed Scythe",
			"True Hallowed Scythe",
			"True Blood Harvest",
			"True Falling Twilight",
			"Illumite Scythe",
			"Demon Blood Ripper",
			"Dread Tearer",
			"Terra Scythe",
			"The Black Scythe",
			"Terrarium Holy Scythe",
			"Christmas Cheer",
			"Reality Slasher",
			"Rotten Cod",
			"The Stalker",
			"Templar's Judgement",
			"Sacred Bludgeon",
			"The Effuser",
			"Shadow Wand",
			"Feather Barrier Rod",
			"Deep Staff",
			"Life Disperser",
			"Omen",
			"Spirit Blast Wand",
			"Midnight Staff",
			"Twilight Staff",
			"Li'l Devil's Wand",
			"Iridescent Staff",
			"Heavenly Cloud Scepter",
			"Hallowed Blessing",
			"Mind Melter",
			"Spirit Bender's Staff",
			"Life and Death",
			"Friendly-Fire Staff",
			"Pagan's Grasp",
			"Celestial Burst Staff",
			"Lucidity",
			"Leech Bolt",
			"Dark Contagion",
			"Blood Transfusion",
			"Wild Umbra",
			"Holy Fire",
			"Pill",
			"Palm Cross",
			"Purified Water",
			"Poison Prickler",
			"Samsara Lotus",
			"Tranquil Lyre",
			"Pill Popper",
			"Holy Hammer"
		};

		public static List<String> symphonicWeapons = new List<String>()
		{
			"Gold Bugle Horn",
			"Platinum Bugle Horn",
			"Hot Horn",
			"Bone Trumpet",
			"Trombone",
			"Cadaver's Cornet",
			"24-Carat Tuba",
			"Prime's Roar",
			"Ghastly French Horn",
			"Sousaphone",
			"Antlion Maraca",
			"Drum Mallet",
			"Tambourine",
			"Seashell Castanets‎",
			"Ebon Wood Tambourine",
			"Shade Wood Tambourine",
			"Bronze Tuning Fork",
			"Bongos",
			"Aquamarine Wineglass",
			"Frostwind Cymbals",
			"Vinyl Record",
			"Hell's Bell",
			"Wind Chimes",
			"The Triangle",
			"The Green Tambourine",
			"Turtle Drums",
			"The Set",
			"Granite Boom Box",
			"Microphone",
			"Sonar Cannon",
			"Conch Shell",
			"Kazoo",
			"Midnight Bass Booster",
			"Hallowed Megaphone",
			"Shadowflame Warhorn",
			"The Maw",
			"Terrarium Autoharp",
			"The Black Otamatone",
			"Sound Sage's Lament",
			"Sonic Amplifier",
			"Black MIDI",
			"Wooden Whistle",
			"Harmonica",
			"Flute",
			"Honey Recorder",
			"Didgeridoo",
			"Meteorite Oboe",
			"Glacial Piccolo",
			"Forest Ocarina",
			"Panflute",
			"Infernal Piccolo",
			"Song of Ice & Fire",
			"Bagpipe",
			"Chrono Ocarina",
			"Clarinet",
			"Red Vuvuzela",
			"Yellow Vuvuzela",
			"Green Vuvuzela",
			"Blue Vuvuzela",
			"Lihzahrd Saxophone",
			"Bassoon",
			"Holophonor",
			"Dynasty Guzheng",
			"Skyware Lute",
			"Yew-Wood Lute",
			"Sitar",
			"Acoustic Guitar",
			"Riff Weaver",
			"Violin",
			"Eskimo Banjo",
			"Sunflare Guitar",
			"Fishbone",
			"Strawberry Heart",
			"Siren's Lyre",
			"Rockstar's Double-Bass Blast Guitar",
			"Edge of Imagination"
		};
		#endregion
	}
	
	
}
