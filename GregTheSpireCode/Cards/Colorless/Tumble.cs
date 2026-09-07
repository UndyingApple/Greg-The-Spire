using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

public class Tumble() : GregTheSpireCard(3,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
    ];
    
    public static async Task<IEnumerable<Tumble>> CreateInDraw(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Tumble> Tumbles = Tumble.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) Tumbles, PileType.Draw, creator ?? owner);
        IEnumerable<Tumble> inDraw = Tumbles;
        Tumbles = null;
        return inDraw;
    }
    
    public static IEnumerable<Tumble> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Tumble> TumbleList = new List<Tumble>();
        for (int index = 0; index < amount; ++index)
            TumbleList.Add(combatState.CreateCard<Tumble>(owner));
        return (IEnumerable<Tumble>) TumbleList;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<TumblePower>(choiceContext, this.Owner.Creature, 1, this.Owner.Creature, (CardModel) this);
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}