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
			
			Dictionary<int, string> itemNameOverrides = new Dictionary<int, string>
			{
				{ItemID.AvengerEmblem, "Avengers Emblem"}
			};

			foreach ( KeyValuePair<int, string> kvp in itemNameOverrides)
			{
				int itemId = kvp.Key;
				string replacementName = kvp.Value;
				if (item.type == itemId)
				{
					item.SetNameOverride(replacementName);
				}
			}
			
			if (item.type == ItemID.SpikyBall)
			{
                item.ammo = item.type;
			}

			if (item.type == ItemID.ExplodingBullet || item.Name == "Endless Explosive Pouch")
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
			base.OpenVanillaBag(context, player, arg);
		}
	}
	
	
}
