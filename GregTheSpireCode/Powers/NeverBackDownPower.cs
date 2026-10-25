using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class NeverBackDownPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ConfidencePower>()
    ];

    protected override object InitInternalData() => (object) new NeverBackDownPower.Data();
    
    private class Data
    {
        public int timesConfidenceGainedThisTurn = 0;
    }
    
    public override Task BeforeSideTurnStart(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (!participants.Contains<Creature>(this.Owner))
            return Task.CompletedTask;
        GetInternalData<Data>().timesConfidenceGainedThisTurn = 0;
        return Task.CompletedTask;
    }

    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (amount <= 0M || applier != this.Owner || !(power is ConfidencePower) || this.GetInternalData<Data>().timesConfidenceGainedThisTurn >= this.Amount)
            return;
        
        this.GetInternalData<Data>().timesConfidenceGainedThisTurn++;
        
        this.Flash();
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, 1, this.Owner.Player);
    }

    
}