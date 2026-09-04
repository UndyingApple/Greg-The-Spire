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
    
    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, Decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power != this)
            return;
        await PowerCmd.Apply<StrengthPower>(choiceContext, this.Owner,
            this.Owner.GetPowerAmount<RecklessnessPower>() > 0 ? amount * 2 : amount, // this is + 2N when recklessness is over 0, N otherwise
            applier, cardSource, true);
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress || target != this.Owner || result.UnblockedDamage <= 0)
            return;
        if (Owner.GetPowerAmount<ObliviousPower>() > 0)
            return;
        this.Flash();
        await PowerCmd.Apply<WeakPower>(choiceContext, this.Owner, 1 + (int) (this.Amount / 5), this.Owner, null);
        await PowerCmd.Apply<ConfidencePower>(choiceContext, this.Owner, -Amount, this.Owner,null);
        
    }
}