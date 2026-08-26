using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class TheLastSupper() : GregTheSpireCard(3,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        var flag = true;
        foreach (CardModel card in this.Owner.PlayerCombatState.ExhaustPile.Cards
                     .Where<CardModel>((Func<CardModel, bool>)(c =>
                         c.Keywords.Contains(GregTheSpireKeywords.Snack) &&
                         !c.Keywords.Contains(CardKeyword.Unplayable))).ToList<CardModel>())
        {
            if (this.IsUpgraded)
                CardCmd.Upgrade(card, CardPreviewStyle.None);
            await CardCmd.AutoPlay(choiceContext, card, (Creature)null, skipCardPileVisuals: !flag);
            flag = false;
        }
    }

    protected override void OnUpgrade()
    {

    }
}