using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class SometimesBackDownUpgradedPower() : GregTheSpirePower
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
        if (!(power is ConfidencePower) || amount >= 0)
            return;
        this.Flash();
        await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>) this.CombatState.HittableEnemies, Math.Abs(Amount * amount), ValueProp.Unpowered, this.Owner);
        //IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext, (IEnumerable<Creature>) this.CombatState.HittableEnemies, (Decimal) this.Amount * amount * -1, ValueProp.Unpowered, this.Owner);
    }
}