using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class FreshStrawberries() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        (DynamicVar) new PowerVar<FreshStrawberriesPower>(2M)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => 
    [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Strawberry>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        FreshStrawberriesPower freshStrawberriesPower = await PowerCmd.Apply<FreshStrawberriesPower>(choiceContext, this.Owner.Creature, this.DynamicVars.Power<FreshStrawberriesPower>().BaseValue, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade() => DynamicVars.Power<FreshStrawberriesPower>().UpgradeValueBy(1);
}