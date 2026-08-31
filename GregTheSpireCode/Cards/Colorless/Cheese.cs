using BaseLib.Extensions;
using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Powers;
using GregTheSpire.GregTheSpireCode.Tags;
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
public class Cheese() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        GregTheSpireKeywords.Snack,
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<ConfidencePower>(1)
    ];
    
    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { GregTheSpireTags.Cheese };
    }
    
    public static async Task<IEnumerable<Cheese>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Cheese> cheeses = Cheese.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cheeses, PileType.Hand, creator ?? owner);
        IEnumerable<Cheese> inHand = cheeses;
        cheeses = null;
        return inHand;
    }
    
    public static IEnumerable<Cheese> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Cheese> cheeseList = new List<Cheese>();
        for (int index = 0; index < amount; ++index)
            cheeseList.Add(combatState.CreateCard<Cheese>(owner));
        return (IEnumerable<Cheese>) cheeseList;
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ConfidencePower confidencePower = await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature, DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(1);
    }
}