using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class ChillPillPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        var confidenceAmount = this.Owner.GetPowerAmount<ConfidencePower>();
        if (confidenceAmount != null)
        {
            ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner, -confidenceAmount, Owner, null);
        }
        await PowerCmd.Remove(this);
    }
}