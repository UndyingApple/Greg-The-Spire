using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class NeverBackDownPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;



    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (!(power is ConfidencePower) || amount <= 0)
            return;
        if ((power.Owner == this.Owner && CombatManager.Instance.History.Entries.OfType<PowerReceivedEntry>()
                    .Count((Func<PowerReceivedEntry, bool>)(e =>
                        e.Actor == this.Owner && e.HappenedThisTurn(this.CombatState) && e.Power is ConfidencePower)) <
                this.Amount))
        {
            this.Flash();
            await CardPileCmd.Draw(choiceContext, 1, power.Owner.Player);
        }
    }
}