using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace DoomBubblesMod.Sounds
{
    public class MelodyStickSound : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance,
            float volume, float pan)
        {
            float note = (float) Main.mouseX / (float) Main.screenWidth;
            float dynamic = (float) Main.mouseY / (float) Main.screenHeight;

            soundInstance = Sound.Value.CreateInstance();
            soundInstance.Volume = volume * dynamic * 1.0f;
            soundInstance.Pan = pan;
            soundInstance.Pitch = note;
            return soundInstance;
        }
    }
}