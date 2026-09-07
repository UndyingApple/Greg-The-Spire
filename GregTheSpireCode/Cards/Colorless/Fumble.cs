using System.Collections;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

public class Fumble() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Token,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(3, ValueProp.Move),
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
    ];
    
    public static async Task<IEnumerable<Fumble>> CreateInDraw(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Fumble> Fumbles = Fumble.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) Fumbles, PileType.Draw, creator ?? owner);
        IEnumerable<Fumble> inDraw = Fumbles;
        Fumbles = null;
        return inDraw;
    }
    
    public static IEnumerable<Fumble> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Fumble> FumbleList = new List<Fumble>();
        for (int index = 0; index < amount; ++index)
            FumbleList.Add(combatState.CreateCard<Fumble>(owner));
        return (IEnumerable<Fumble>) FumbleList;
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        int cardsDrawn = CardPile.MaxCardsInHand - this.Owner.PlayerCombatState.Hand.Cards.Count;
        await CardPileCmd.Draw(choiceContext, cardsDrawn, this.Owner);
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(cardsDrawn).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
        IEnumerable<CardModel> cards = await Tumble.CreateInDraw(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in cards)
            {
                CardCmd.Upgrade(card);
            }
        }
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}