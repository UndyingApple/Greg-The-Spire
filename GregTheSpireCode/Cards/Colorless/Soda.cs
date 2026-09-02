using BaseLib.Extensions;
using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;


[Pool(typeof(TokenCardPool))]
public class Soda() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust,
        GregTheSpireKeywords.Snack
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new CardsVar(1)
    ];
    
    public static async Task<IEnumerable<Soda>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Soda> sodas = Soda.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) sodas, PileType.Hand, creator ?? owner);
        IEnumerable<Soda> inHand = sodas;
        sodas = null;
        return inHand;
    }
    
    public static IEnumerable<Soda> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Soda> sodaList = new List<Soda>();
        for (int index = 0; index < amount; ++index)
            sodaList.Add(combatState.CreateCard<Soda>(owner));
        return (IEnumerable<Soda>) sodaList;
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}