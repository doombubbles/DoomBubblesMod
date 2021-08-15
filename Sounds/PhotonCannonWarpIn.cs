using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace DoomBubblesMod.Sounds
{
    public class PhotonCannonWarpIn : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan,
            SoundType type)
        {
            soundInstance.Volume = volume * 1.0f;
            soundInstance.Pan = pan;
            return soundInstance;
        }
    }
}