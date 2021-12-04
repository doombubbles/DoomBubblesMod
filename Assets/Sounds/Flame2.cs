using Microsoft.Xna.Framework.Audio;

namespace DoomBubblesMod.Assets.Sounds;

public class Flame2 : ModSound
{
    public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan)
    {
        soundInstance = Sound.Value.CreateInstance();
        soundInstance.Volume = volume * 1.0f;
        soundInstance.Pan = pan;
        return soundInstance;
    }
}