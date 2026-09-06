using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Buffet() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [

    ];

    public IEnumerable<CardModel> SnackTokens =>
    [
        ModelDb.Card<Strawberry>(),
        ModelDb.Card<Soda>(),
        ModelDb.Card<Olive>(),
        ModelDb.Card<Cheese>(),
        ModelDb.Card<Cracker>(),
        ModelDb.Card<Fly>(),
        ModelDb.Card<Soup>()
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        int num = CardPile.MaxCardsInHand - CardPile.GetCards(this.Owner, PileType.Hand).Count<CardModel>();
        List<CardModel> cards = new List<CardModel>();
        for (int index = 0; index < num; ++index)
            await CardPileCmd.AddGeneratedCardsToCombat(
                (IEnumerable<CardModel>)CardFactory
                    .GetDistinctForCombat(this.Owner, SnackTokens, 1, Owner.RunState.Rng.CombatCardGeneration)
                    .ToList<CardModel>(), PileType.Hand, this.Owner);




    }
}