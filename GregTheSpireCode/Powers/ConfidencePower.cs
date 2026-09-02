using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class ConfidencePower() : GregTheSpirePower
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override bool AllowNegative => true;
    
    public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
    {
        await PowerCmd.Apply<StrengthPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), target, target.GetPowerAmount<RecklessnessPower>() > 0 ? amount * 2 : amount, applier, cardSource);
    }
    
    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (amount == (Decimal) this.Amount || power != this)
            return;
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner, this.Owner.GetPowerAmount<RecklessnessPower>() > 0 ? amount * 2 : amount, applier, cardSource, true);
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress || target != this.Owner || result.UnblockedDamage <= 0)
            return;
        this.Flash();
        await PowerCmd.Remove((PowerModel) this);
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner, this.Owner.GetPowerAmount<RecklessnessPower>() > 0 ? -this.Amount * 2 : -this.Amount, this.Owner, null);
        await PowerCmd.Apply<WeakPower>(choiceContext, this.Owner, 1 + (int) (this.Amount / 5), this.Owner, null);
    }
}