using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace DoomBubblesMod.Sounds
{
    public class Flame : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan,
            SoundType type)
        {
            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            return soundInstance;
        }
    }
}