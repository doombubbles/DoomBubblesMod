using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace DoomBubblesMod.Sounds
{
    public class GauntletComplete : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan,
            SoundType type)
        {
            soundInstance.Volume = volume * 2.0f;
            soundInstance.Pan = pan;
            return soundInstance;
        }
    }
}