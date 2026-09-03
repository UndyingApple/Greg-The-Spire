using Godot;
using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;


public class Dump() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(5, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        List<CardModel> list = StashCardPile.StashPileType.GetPile(this.Owner).Cards.ToList<CardModel>();
        int cardCount = list.Count;
        foreach (CardModel card in list)
        {
            await CardCmd.Discard(choiceContext, card);
        }

        float scale = 0.8f;
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).WithHitCount(cardCount)
            .FromCard((CardModel)this, play).Targeting(play.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}