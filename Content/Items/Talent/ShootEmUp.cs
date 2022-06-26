using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class ShootEmUp : TalentItem<PhotonCannonStaff>
{
    public override string Description => "Cannons attack twice as fast\n" +
                                          "[Right Click on a Photon Cannon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}