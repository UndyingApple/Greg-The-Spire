using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class OliveCreamsAndASoda() : GregTheSpire.GregTheSpireCode.Cards.GregTheSpireCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self) {
protected override IEnumerable<DynamicVar> CanonicalVars => [];

protected override IEnumerable<IHoverTip> ExtraHoverTips =>
[
    HoverTipFactory.FromCard<Soda>(IsUpgraded),
    HoverTipFactory.FromCard<Olive>(IsUpgraded),
];

protected override async Task OnPlay(
    PlayerChoiceContext choiceContext,
    CardPlay play)
{
    IEnumerable<CardModel> soda = await Soda.CreateInHand(Owner, 1, CombatState);
    if (IsUpgraded)
    {
        foreach (CardModel card in soda)
        {
            CardCmd.Upgrade(card);
        }
    }
    await Cmd.Wait(0.1f);
    int num = CardPile.MaxCardsInHand - CardPile.GetCards(this.Owner, PileType.Hand).Count<CardModel>();
    List<CardModel> cards = new List<CardModel>();
    for (int index = 0; index < num; ++index)
         cards.Add((CardModel) this.CombatState.CreateCard<Olive>(this.Owner));
    if (IsUpgraded)
    {
        foreach (Soda card in cards)
        {
            CardCmd.Upgrade(card);
        }
    }
    IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cards, PileType.Hand, this.Owner);
}

protected override void OnUpgrade()
{

}
}