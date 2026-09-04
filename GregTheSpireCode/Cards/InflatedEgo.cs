using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class InflatedEgo() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        (DynamicVar)new PowerVar<ConfidencePower>(5)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
      await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature,
            DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(2);
    } 
}    