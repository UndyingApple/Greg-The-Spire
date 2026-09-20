using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class PositiveAffirmation() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<ConfidencePower>(2)
    ];
    
        protected override bool HasEnergyCostX => true;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ConfidencePower>(),
        
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int count = this.ResolveEnergyXValue();
        for (int i = 0; i < count; ++i)
            await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(1);
    }
}
 