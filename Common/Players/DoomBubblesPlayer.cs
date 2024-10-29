using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Common.Configs;
using Terraria;
using Terraria.ID;

namespace DoomBubblesMod.Common.Players;

public class DoomBubblesPlayer : ModPlayer
{
    public float critChanceMult = 1f;
    public bool crystalBulletBonus;
    public int emblem;
    public bool explosionBulletBonus;

    public bool homing;
    public bool luminiteBulletBonus;
    public bool noManaFlower;

    public List<int> NoManaItems { get; } = [];
    public bool sStone;
    public bool united;
    public bool vampireKnifeBat;

    public bool autoShoot;

    public int weaponDye;

    public override void ResetEffects()
    {
        homing = false;
        crystalBulletBonus = false;
        explosionBulletBonus = false;
        luminiteBulletBonus = false;
        sStone = false;
        noManaFlower = false;
        critChanceMult = 1f;
        emblem = 0;
        vampireKnifeBat = false;
        united = false;
        autoShoot = false;
        weaponDye = 0;

        NoManaItems.Clear();


        if (GetInstance<ServerConfig>().AutoTorchGod) Player.unlockedBiomeTorches = true;
    }

    public override void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
        if (explosionBulletBonus &&
            modifiers.DamageSource.SourceProjectileType == ProjectileID.ExplosiveBullet &&
            modifiers.DamageSource.SourcePlayerIndex == Player.whoAmI)
        {
           modifiers.FinalDamage *= 0;
        }

        base.ModifyHurt(ref modifiers);
    }

    public override void PostUpdateEquips()
    {
        var uniteds = Main.player.Count(p => p.active && !p.dead && p.GetModPlayer<DoomBubblesPlayer>().united);

        Player.GetDamage(DamageClass.Generic) += .1f * uniteds;
        if (noManaFlower)
        {
            Player.manaFlower = false;
        }
    }

    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod,
        bool mediumCoreDeath)
    {
        var item = new Item();
        item.SetDefaults(ItemID.CopperBroadsword);
        item.Prefix(-1);
        itemsByMod["Terraria"][0].TurnToAir();
        itemsByMod["Terraria"][0] = item;
    }

    public override bool CanConsumeAmmo(Item weapon, Item ammo) => !Player.GetModPlayer<DoomBubblesPlayer>().homing;
}