using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Enchantments;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class WaiterWaiter() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        CardModel card;
        if (IsUpgraded)
        {
            card = (await CardSelectCmd.FromHand(choiceContext, Owner, 
                new CardSelectorPrefs(InfestedSelectorPrefs.InfestedSelectionPrompt, 1, 1),
                null,
                this)).ToList()[0];
        }
        else
        {
            card = Owner.RunState.Rng.Shuffle.NextItem<CardModel>((IEnumerable<CardModel>) PileType.Hand.GetPile(Owner).Cards.Where<CardModel>((Func<CardModel, bool>) (c => c.Enchantment == null && !c.Keywords.Contains(CardKeyword.Unplayable))).ToList<CardModel>());
        }

        if (card != null && card.Enchantment == null)
        {
            CardCmd.Enchant<Infested> (card, 1);
            CardCmd.Preview(card);
        }
    }

    protected override void OnUpgrade()
    {

    }
}

public struct InfestedSelectorPrefs {
    public static LocString InfestedSelectionPrompt => new LocString("card_selection", "GREGTHESPIRE-INFEST");
}