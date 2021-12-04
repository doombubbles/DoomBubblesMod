using System;
using DoomBubblesMod.Content.Items.Talent;
using DoomBubblesMod.Content.Projectiles.HotS;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace DoomBubblesMod.Content.Items.HotS;

public class DiscordBlade : ModItemWithTalents<TalentChaosReigns, TalentDissonance, TalentLethalOnslaught>
{
    protected override Color? TalentColor => Color.Red;

    private float Length => ChosenTalent == 2 || ChosenTalent == -1 ? 600f : 300f;

    public override bool OnlyShootOnSwing => false;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Discord Blade");
        Tooltip.SetDefault("Makes a very hard to code triangle attack\n" +
                           "Magic and Melee Weapon");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        Item.damage = 75;
        Item.DamageType = DamageClass.Magic;
        Item.width = 36;
        Item.height = 44;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.value = 640000;
        Item.rare = ItemRarityID.Yellow;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<DiscordStrike>();
        Item.shootSpeed = 10f;
        Item.scale = 1.2f;
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
            //Emit dusts when swing the sword
            var dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.TheDestroyer);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].noLight = false;
        }
    }

    public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        if (player == Main.LocalPlayer)
        {
            var dX = Main.MouseWorld.X - player.Center.X;
            var dY = Main.MouseWorld.Y - player.Center.Y;
            var theta = new Vector2(dX, dY).ToRotation();

            for (var i = -1; i <= 1; i++)
            {
                position = player.Center;
                velocity.X = (float) Math.Cos(theta + i * (Math.PI / 2)) * Length / 15f;
                velocity.Y = (float) Math.Sin(theta + i * (Math.PI / 2)) * Length / 15f;
                if (i != 0)
                {
                    velocity.X /= 3f;
                    velocity.Y /= 3f;
                    position += 1200f / Length * velocity;
                }
                else
                {
                    position += 300f / Length * velocity;
                }

                var proj = Projectile.NewProjectile(source, position, velocity, type, damage,
                    knockback * 2, player.whoAmI, ChosenTalent);
                Main.projectile[proj].netUpdate = true;
            }
        }

        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/DiscordStrike"), position);

        return false;
    }
}