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
				{"Celestial Burst Staff", "Heavenly Burst Staff"}
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
			
			/*
			
			if (item.Name == "Waifu in a Bottle")
			{
				item.SetNameOverride("Weeaboo in a Bottle");
				if (item.owner != 255)
				{
					if (Main.player[item.owner].name == "Ron F***ing Swanson")
					{
						item.SetNameOverride("Tammy I in a Bottle");
					}
					else if (Main.player[item.owner].name == "Ben Wyatt")
					{
						item.SetNameOverride("Leslie in a Bottle");
					}
					else if (Main.player[item.owner].name == "Tom Haverford")
					{
						item.SetNameOverride("Lucy in a Bottle");
					}
				}
			}
			if (item.Name == "Rare Waifu in a Bottle")
			{
				item.SetNameOverride("Rare Weeaboo in a Bottle");
				if (item.owner != 255)
				{
					if (Main.player[item.owner].name == "Ron F***ing Swanson")
					{
						item.SetNameOverride("Tammy II in a Bottle");
					}
					else if (Main.player[item.owner].name == "Ben Wyatt")
					{
						item.SetNameOverride("Rare Leslie in a Bottle");
					}
					else if (Main.player[item.owner].name == "Tom Haverford")
					{
						item.SetNameOverride("Rare Lucy in a Bottle");
					}
				}
			}*/
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

		public override void GetWeaponDamage(Item item, Player player, ref int damage)
		{
			if ((item.type == ItemID.ExplodingBullet || item.Name == "Endless Explosive Pouch") &&
			    player.GetModPlayer<DoomBubblesPlayer>().explosionBulletBonus)
			{
				damage *= 2;
			}

			if (radiantWeapons.Contains(item.Name))
			{
				damage += (int) (item.damage * (player.GetModPlayer<DoomBubblesPlayer>().customRadiantDamage - 1f));
			}

			if (symphonicWeapons.Contains(item.Name))
			{
				damage += (int) (item.damage * (player.GetModPlayer<DoomBubblesPlayer>().customSymphonicDamage - 1f));
			}
			base.GetWeaponDamage(item, player, ref damage);
		}

		public override void PostReforge(Item item)
		{
			if (Main.player[item.owner].GetModPlayer<DoomBubblesPlayer>().reforgeCheatCodes && item.accessory)
			{
				Main.NewText(item.prefix);
				if (item.prefix == PrefixID.Hard )
				{
					item.rare += 2;
					item.prefix = PrefixID.Warding;
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				} 
				else if (item.prefix == PrefixID.Guarding)
				{
					item.rare++;
					item.prefix = PrefixID.Warding;
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				}
				else if (item.prefix == PrefixID.Armored)
				{
					item.rare++;
					item.prefix = PrefixID.Warding;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				
				else if (item.prefix == mod.PrefixType("Mysterious"))
				{
					item.rare += 2;
					item.prefix = mod.PrefixType("Sorcerous");
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana = 40;
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				} 
				else if (item.prefix == PrefixID.Arcane)
				{
					item.rare++;
					item.prefix = mod.PrefixType("Sorcerous");
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana = 40;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				else if (item.prefix == mod.PrefixType("Mystical"))
				{
					item.rare++;
					item.prefix = mod.PrefixType("Sorcerous");
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana = 40;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				
				else if (item.prefix == PrefixID.Precise)
				{
					item.rare++;
					item.prefix = PrefixID.Lucky;
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				} 
				
				else if (item.prefix == PrefixID.Jagged)
				{
					item.rare += 2;
					item.prefix = PrefixID.Menacing;
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				} 
				else if (item.prefix == PrefixID.Spiked)
				{
					item.rare++;
					item.prefix = PrefixID.Menacing;
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				}
				else if (item.prefix == PrefixID.Angry)
				{
					item.rare++;
					item.prefix = PrefixID.Menacing;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				
				else if (item.prefix == PrefixID.Brisk)
				{
					item.rare += 2;
					item.prefix = PrefixID.Quick2;
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				} 
				
				else if (item.prefix == PrefixID.Fleeting)
				{
					item.rare++;
					item.prefix = item.prefix = PrefixID.Quick2;
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				}
				else if (item.prefix == PrefixID.Hasty2)
				{
					item.rare++;
					item.prefix = item.prefix = PrefixID.Quick2;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				
				else if (item.prefix == PrefixID.Wild)
				{
					item.rare += 2;
					item.prefix = PrefixID.Violent;
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				} 
				else if (item.prefix == PrefixID.Rash)
				{
					item.rare++;
					item.prefix = PrefixID.Violent;
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				}
				else if (item.prefix == PrefixID.Intrepid)
				{
					item.rare++;
					item.prefix = PrefixID.Violent;
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
				
				else if (item.prefix == mod.PrefixType("Opportune"))
				{
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().critDamage = 10;
					item.rare++;
					item.prefix = mod.PrefixType("Decisive");
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				} 
				
				else if (item.prefix == mod.PrefixType("Fresh"))
				{
					item.rare += 2;
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp = 20;
					item.prefix = mod.PrefixType("Vigorous");
					item.value = 10 * (int) Math.Round((item.value / 1.1025 * 1.44) / 10);
				}
				else if (item.prefix == mod.PrefixType("Tough"))
				{
					item.rare++;
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp = 20;
					item.prefix = mod.PrefixType("Vigorous");
					item.value = 10 * (int) Math.Round((item.value / 1.21 * 1.44) / 10);
				}
				else if (item.prefix == mod.PrefixType("Healthy"))
				{
					item.rare++;
					item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp = 20;
					item.prefix = mod.PrefixType("Vigorous");
					item.value = 10 * (int) Math.Round((item.value / 1.3225 * 1.44) / 10);
				}
			}
			
			base.PostReforge(item);
		}

		public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
		{
			if (Main.player[item.owner].GetModPlayer<DoomBubblesPlayer>().reforgeCheatCodes && item.accessory)
			{
				reforgePrice *= 2;
			}
			return base.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
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


		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (item.social && !item.accessory)
			{
				string secondSetBonus = Main.player[item.owner].GetModPlayer<DoubleSetBonuses>().secondSetBonus;
				if (secondSetBonus != "")
				{
					tooltips.Add(new TooltipLine(mod, "SetBonusCheat", "Set Bonus: " + secondSetBonus));
				}
			}
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
