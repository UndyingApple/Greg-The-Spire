using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class TheresAFlyInMySoupPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override int DisplayAmount
    {
        get => Math.Max(0, this.Amount - this.GetInternalData<Data>().snacksPlayed);
    }
    
    protected override object InitInternalData() => new Data();
    
    public override Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        this.SetSnacksPlayed(CombatManager.Instance.History.Entries.OfType<CardPlayStartedEntry>().Count<CardPlayStartedEntry>((Func<CardPlayStartedEntry, bool>) (e => e.CardPlay.Card.Keywords.Contains(GregTheSpireKeywords.Snack) && e.CardPlay.Player == this.Owner.Player && e.HappenedThisTurn(this.CombatState))));
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != this.Owner || !cardPlay.Card.Keywords.Contains(GregTheSpireKeywords.Snack) || cardPlay.Card.IsDupe || this.GetInternalData<TheresAFlyInMySoupPower.Data>().snacksPlayed >= this.Amount)
            return;
        await Fly.CreateInHand(Owner.Player, 1, CombatState);
        this.Flash();
        this.SetSnacksPlayed(this.GetInternalData<Data>().snacksPlayed + 1);
        this.InvokeDisplayAmountChanged();
    }

    public override Task AfterSideTurnStart(
        CombatSide side,
        IReadOnlyList<Creature> participants,
        ICombatState combatState)
    {
        if (!participants.Contains<Creature>(this.Owner))
            return Task.CompletedTask;
        this.SetSnacksPlayed(0);
        this.InvokeDisplayAmountChanged();
        return Task.CompletedTask;
    }
    
    private void SetSnacksPlayed(int value)
    {
        this.GetInternalData<TheresAFlyInMySoupPower.Data>().snacksPlayed = value;
        this.InvokeDisplayAmountChanged();
    }

    private class Data
    {
        public int snacksPlayed;
    }
}