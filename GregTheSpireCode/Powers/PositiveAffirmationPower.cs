using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class PositiveAffirmationPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterAutoPostPlayPhaseEntered(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (this.Owner.GetPowerAmount<WeakPower>() > 0)
        {
            Flash();
            await PowerCmd.Apply<ConfidenceNextTurnPower>(choiceContext, this.Owner, this.Amount, this.Owner, null);
        }
    }
}