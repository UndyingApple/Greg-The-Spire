using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class ChillPill() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        var confidenceAmount = this.Owner.Creature.GetPowerAmount<ConfidencePower>();
        if (confidenceAmount != null)
        {
            ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, -confidenceAmount, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Retain);
}