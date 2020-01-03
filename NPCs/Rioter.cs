using System;
using System.Collections.Generic;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace DoomBubblesMod.NPCs
{
	// [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
	public class Rioter : ModNPC
	{
		public override string Texture => "DoomBubblesMod/NPCs/Rioter";

		public override bool Autoload(ref string name) {
			name = "Rioter";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() {
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			DisplayName.SetDefault("Rioter");
			Main.npcFrameCount[npc.type] = 23;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Mechanic;
		}

		public override void HitEffect(int hitDirection, double damage) {
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++) {
				//Dust.NewDust(npc.position, npc.width, npc.height, DustType<Sparkle>());
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
			return System.IO.Directory.Exists(@"C:\Riot Games\League of Legends") ||
				System.IO.File.Exists(@"/Applications/League of Legends.app");
		}

		public override string TownNPCName() {
			WeightedRandom<string> name = new WeightedRandom<string>();
			name.Add("Morello");
			name.Add("Guinsoo");
			name.Add("Shurelya");
			name.Add("Randuin");
			name.Add("Runaan");
			name.Add("Phreak");
			name.Add("CertainlyT");
			name.Add("Aether");
			return name;
		}

		public override void FindFrame(int frameHeight) {
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}
		
		// Consider using this alternate approach to choosing a random thing. Very useful for a variety of use cases.
		// The WeightedRandom class needs "using Terraria.Utilities;" to use
		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();
			if (npc.GivenName == "Phreak") chat.Add("Tons of damage.", 3);
			chat.Add("Better nerf Irelia.");
			chat.Add("Welcome to Summoner's Rift! Oh, wait...");
			chat.Add("Twisted Treeline? Never heard of it.");
			chat.Add("The Crystal Scar? Never heard of it.");
			chat.Add("Every third auto atta-Hey! Can't you see I'm designing here?");
			chat.Add("Who even is Doran anyway?");
			chat.Add("Hm, I've never been to this part of Runeterra.");
			chat.Add("Is Kassadin banned here?");
			if (Main.LocalPlayer.coldDash || Main.LocalPlayer.eocDash != 0 || Main.LocalPlayer.sailDash ||
			    Main.LocalPlayer.dash != 0 || Main.LocalPlayer.solarShields > 0)
			{
				chat.Add("That's a very nice dash you've got there...", 3);
			}
			if (NPC.FindFirstNPC(NPCID.Pirate) >= 0)
			{
				chat.Add("Watch out! It's Gangplank! Oh, no, wait, that's just " + Main.npc[NPC.FindFirstNPC(NPCID.Pirate)].GivenName + ".", 2);
			}
			if (NPC.FindFirstNPC(NPCID.Dryad) >= 0)
			{
				chat.Add("I think " + Main.npc[NPC.FindFirstNPC(NPCID.Dryad)].GivenName + " would really hit it off with my friend Ivern.", 2);
			}
			if (NPC.FindFirstNPC(NPCID.Truffle) >= 0)
			{
				chat.Add("I think " + Main.npc[NPC.FindFirstNPC(NPCID.Truffle)].GivenName + " would really hit it off with my friend Ivern.", 2);
			}
			if (NPC.FindFirstNPC(NPCID.Nurse) >= 0)
			{
				chat.Add("Why is " + Main.npc[NPC.FindFirstNPC(NPCID.Nurse)].GivenName + " here anyway? Do you guys not have Fountains?", 2);
			}
			if (NPC.FindFirstNPC(NPCID.TravellingMerchant) >= 0)
			{
				chat.Add("A Travelling Merchant! So that's how Ornn does it.", 2);
			}
			chat.Add("");
			return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
		}
		

		public override void SetChatButtons(ref string button, ref string button2) {
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = "Reforge Runes";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			if (firstButton) {
				shop = true;
			}
			else {
				// If the 2nd button is pressed, open the inventory...
				Main.playerInventory = true;
				// remove the chat window...
				Main.npcChatText = "";
				// and start an instance of our UIState.
				// Note that even though we remove the chat window, Main.LocalPlayer.talkNPC will still be set correctly and we are still technically chatting with the npc.
				GetInstance<DoomBubblesMod>().RunesUserInterface.SetState(new RunesUI());
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			List<String> basicItems = new List<string>
			{
				"FaerieCharm", "RejuvenationBead", "ClothArmor", "Dagger", "DarkSeal", "LongSword", "SapphireCrystal",
				"RubyCrystal", "AmplifyingTome", "NullMagicMantle", "StopwatchOfLegends", "CloakOfAgility", 
				"BlastingWand", "PickaxeOfLegends", "NeedlesslyLargeRod", "BFSword"
			};
			shop.item[nextSlot].SetDefaults(mod.ItemType("RunePage"));
			nextSlot++;
			foreach (string basicItem in basicItems)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType(basicItem));
				nextSlot++;
			}
		}

		public override void NPCLoot() {
			//Item.NewItem(npc.getRect(), ItemType<Items.Armor.ExampleCostume>());
		}

		// Make this Town NPC teleport to the King and/or Queen statue when triggered.
		public override bool CanGoToStatue(bool toKingStatue) {
			return true;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 10;
			knockback = 8f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)//Allows you to customize how this town NPC's weapon is drawn when this NPC is swinging it (this NPC must have an attack type of 3). ItemType is the Texture2D instance of the item to be drawn (use Main.itemTexture[id of item]), itemSize is the width and height of the item's hitbox
		{
			scale = 1f;
			item = Main.itemTexture[mod.ItemType("InfinityEdge")]; //this defines the item that this npc will use
			itemSize = 40;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight) //  Allows you to determine the width and height of the item this town NPC swings when it attacks, which controls the range of this NPC's swung weapon.
		{
			itemWidth = 50;
			itemHeight = 50;
		}
		
	}
}
