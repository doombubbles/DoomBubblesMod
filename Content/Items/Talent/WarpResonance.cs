using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class WarpResonance : TalentItem<PhotonCannonStaff>
{
    public override string Description => "Cannons no longer require Pylon Power, but are buffed by it\n" +
                                          "[Right Click on a Photon Cannon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}