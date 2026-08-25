using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Relics;

public class TrustyBackpack() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Starter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StoragePower>()
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("Rounds", 1),
        new PowerVar<StoragePower>(3)
    ];


    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner || player.PlayerCombatState?.TurnNumber > DynamicVars["Rounds"].IntValue) return;
        await PowerCmd.Apply<StoragePower>(
            new ThrowingPlayerChoiceContext(), Owner.Creature,
            DynamicVars.Power<StoragePower>().BaseValue, Owner.Creature, null);
    }
    
}