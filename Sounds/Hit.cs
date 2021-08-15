using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Sounds
{
    public class Hit : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan,
            SoundType type)
        {
            soundInstance.Volume = volume * 1.0f;
            soundInstance.Pan = pan;
            soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
            return soundInstance;
        }
    }
}