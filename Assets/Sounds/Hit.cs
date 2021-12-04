using Microsoft.Xna.Framework.Audio;

namespace DoomBubblesMod.Assets.Sounds;

public class Hit : ModSound
{
    public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan)
    {
        soundInstance = Sound.Value.CreateInstance();
        soundInstance.Volume = volume * 1.0f;
        soundInstance.Pan = pan;
        soundInstance.Pitch = Main.rand.Next(-5, 6) * .05f;
        return soundInstance;
    }
}