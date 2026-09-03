using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards.Multiplayer;

public class PepTalk() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyAlly)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ConfidencePower>(5)
    ];

    public override CardMultiplayerConstraint MultiplayerConstraint
    {
        get => CardMultiplayerConstraint.MultiplayerOnly;
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [  
        HoverTipFactory.FromPower<ConfidencePower>()
    ];

       

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
  
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await PowerCmd.Apply<ConfidencePower>(choiceContext, cardPlay.Target, DynamicVars.Power<ConfidencePower>().BaseValue, this.Owner.Creature, (CardModel) this);
    
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(2);
    }
}