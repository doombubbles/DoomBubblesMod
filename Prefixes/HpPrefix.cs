using System.Runtime.InteropServices;
using DoomBubblesMod.Items;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Prefixes
{
	public class HpPrefix : ModPrefix
	{
		private readonly byte hp = 0;

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
		
		public HpPrefix()
		{
		}

		public HpPrefix(byte hp)
		{
			this.hp = hp;
		}

		// Allow multiple prefix autoloading this way (permutations of the same prefix)
		public override bool Autoload(ref string name)
		{
			if (base.Autoload(ref name))
			{
				mod.AddPrefix("Fresh", new HpPrefix(5));
				mod.AddPrefix("Tough", new HpPrefix(10));
				mod.AddPrefix("Healthy", new HpPrefix(15));
				mod.AddPrefix("Vigorous", new HpPrefix(20));
			}
			return false;
		}

		public override void Apply(Item item)
		{
			item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp = hp;
		}

		
		public override void ModifyValue(ref float valueMult)
		{
			valueMult *= 1f + hp * .01f;
		}
		
	}
}