using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class NegativeAffirmation() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0),
        new CalculationExtraVar(3),
            
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier(((Func<CardModel, Creature, Decimal>) ((card, _) =>
        {
            return (Decimal) 1;
        })))
    ];
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        var confidenceAmount = this.Owner.Creature.GetPowerAmount<ConfidencePower>();
        Decimal change = confidenceAmount / 2;
        if (confidenceAmount != null)
        {
            ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, this.Owner.Creature, -change, Owner.Creature, this);
        }
        Decimal num = await CreatureCmd.GainBlock(this.Owner.Creature, confidenceAmount / 2 * this.DynamicVars.CalculatedBlock.Calculate(play.Target), this.DynamicVars.CalculatedBlock.Props, play);
        
        
    }

    protected override void OnUpgrade() => DynamicVars.CalculationExtra.UpgradeValueBy(1);
}