using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Smug() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<ConfidencePower>(2)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(this.CreateClone(), PileType.Discard, this.Owner), 2.2f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(1);
    }
}