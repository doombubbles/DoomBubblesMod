using Microsoft.Xna.Framework.Audio;

namespace DoomBubblesMod.Assets.Sounds;

public class MelodyStickSound : ModSound
{
    public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance,
        float volume, float pan)
    {
        var note = Main.mouseX / (float) Main.screenWidth;
        var dynamic = Main.mouseY / (float) Main.screenHeight;

        soundInstance = Sound.Value.CreateInstance();
        soundInstance.Volume = volume * dynamic * 1.0f;
        soundInstance.Pan = pan;
        soundInstance.Pitch = note;
        return soundInstance;
    }
}