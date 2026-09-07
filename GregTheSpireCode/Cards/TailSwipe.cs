using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class TailSwipe() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [ 
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedVar("CalculatedCards").WithMultiplier((Func<CardModel,
        Creature, Decimal >) ((card, _) => (Decimal) StashCardPile.StashPileType.GetPile(card.Owner).Cards.Count))
    ];
    
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.AttackAnimDelay);
        await CardPileCmd.Draw(choiceContext, ((CalculatedVar) this.DynamicVars["CalculatedCards"]).Calculate(play.Target), this.Owner);
        
        if(IsUpgraded) await PlayFromStashCmd.PlayFromStashCmdAsync(choiceContext, this.Owner, 1, 1,null);
    }

    protected override void OnUpgrade()
    {
        
    }
}