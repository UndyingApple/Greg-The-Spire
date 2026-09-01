using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Precisely() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("DivideAmt", 2)
    ];
    
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => 
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        var confidenceAmount = this.Owner.Creature.GetPowerAmount<ConfidencePower>();
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, (int)(confidenceAmount / ((int) this.DynamicVars["DivideAmt"].BaseValue)), Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["DivideAmt"].UpgradeValueBy(-1);
    }
}