using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

 
public class EGOMANIAPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    

    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner.Player)
            return;
        await PowerCmd.Apply<ConfidencePower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), this.Owner, (Decimal) this.Amount, this.Owner, (CardModel) null);
        ;
    }
    
    
    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!CombatManager.Instance.IsInProgress || target != this.Owner || result.UnblockedDamage <= 0)
            return;
        this.Flash();
        var confidenceAmount = this.Owner.Player.Creature.GetPowerAmount<ConfidencePower>();
        await PowerCmd.Apply<VulnerablePower>(choiceContext, this.Owner, 1 + (int) (confidenceAmount / 5), this.Owner, null);

    }
}