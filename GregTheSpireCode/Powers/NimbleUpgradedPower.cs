using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class NimbleUpgradedPower() : GregTheSpirePower
{

    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return  [new EnergyVar(2)];
        }
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
    [
        HoverTipFactory.ForEnergy((PowerModel) this)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != this.Owner || cardPlay.Card.EnergyCost.GetResolved() < this.DynamicVars.Energy.IntValue)
            return;
        if ((cardPlay.Card is Nimble) && (!cardPlay.Card.IsUpgradable))
        {
            Flash();
            await StealCmd.StealAsync(choiceContext, cardPlay.Card.Owner, (Amount-1));
            return;
        }
                    
        this.Flash();
        await StealCmd.StealAsync(choiceContext, cardPlay.Card.Owner, Amount);
    }
}
