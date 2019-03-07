using System.Runtime.InteropServices;
using DoomBubblesMod.Items;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Prefixes
{
	public class ManaPrefix : ModPrefix
	{
		private readonly byte mana = 0;

		// see documentation for vanilla weights and more information
		// note: a weight of 0f can still be rolled. see CanRoll to exclude prefixes.
		// note: if you use PrefixCategory.Custom, actually use ChoosePrefix instead, see ExampleInstancedGlobalItem
		public override float RollChance(Item item)
		{
			return 1f;
		} 

		// determines if it can roll at all.
		// use this to control if a prefixes can be rolled or not
		public override bool CanRoll(Item item)
		{
			return true;
		}

		// change your category this way, defaults to Custom
		public override PrefixCategory Category { get { return PrefixCategory.Accessory; } }
		
		public ManaPrefix()
		{
		}

		public ManaPrefix(byte mana)
		{
			this.mana = mana;
		}

		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				mod.AddPrefix("Mysterious", new ManaPrefix(10));
				mod.AddPrefix("Mystical", new ManaPrefix(30));
				mod.AddPrefix("Sorcerous", new ManaPrefix(40));
			}
			return false;
		}

		public override void Apply(Item item)
		{
			item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana = mana;
		}

		
		public override void ModifyValue(ref float valueMult)
		{
			if (mana == 10)
			{
				valueMult *= 1.05f;
			}
			else if (mana == 30)
			{
				valueMult *= 1.15f;
			}
			else if (mana == 40)
			{
				valueMult *= 1.2f;
			}
		}
		
	}
}