using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Precisely() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ConfidencePower>(1)
    ];
    
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => 
    [
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ConfidencePower>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
        var confidenceAmount = this.Owner.Creature.GetPowerAmount<ConfidencePower>();
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, (int)(Math.Abs(confidenceAmount)), Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["DivideAmt"].UpgradeValueBy(-1);
    }
}