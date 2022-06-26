using DoomBubblesMod.Content.Items.HotS;

namespace DoomBubblesMod.Content.Items.Talent;

public class TowerDefense : TalentItem<PhotonCannonStaff>
{
    public override string Description => "Cannons only take up half a minion slot\n" +
                                          "[Right Click on a Photon Cannon Staff with this to apply]";

    protected override int RarityColor => ItemRarityID.Yellow;
}