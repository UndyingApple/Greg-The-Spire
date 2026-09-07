using BaseLib.Extensions;
using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
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
public class CheeseBall() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain,
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        (DynamicVar) new PowerVar<ConfidencePower>(1)
    ];

    public void IncreaseConfidence()
    {
        if (IsUpgraded) DynamicVars.Power<ConfidencePower>().BaseValue += 2;
        else DynamicVars.Power<ConfidencePower>().BaseValue += 1;
    }
    
    public static async Task<IEnumerable<CheeseBall>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<CheeseBall> cheeseBalls = CheeseBall.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) cheeseBalls, PileType.Hand, creator ?? owner);
        IEnumerable<CheeseBall> inHand = cheeseBalls;
        cheeseBalls = null;
        return inHand;
    }
    
    public static IEnumerable<CheeseBall> Create(Player owner, int amount, ICombatState combatState)
    {
        List<CheeseBall> cheeseBallList = new List<CheeseBall>();
        for (int index = 0; index < amount; ++index)
            cheeseBallList.Add(combatState.CreateCard<CheeseBall>(owner));
        return (IEnumerable<CheeseBall>) cheeseBallList;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature,
            DynamicVars.Power<ConfidencePower>().BaseValue, Owner.Creature, this);
    }
    
    protected override void OnUpgrade()
    {
        DynamicVars.Power<ConfidencePower>().UpgradeValueBy(1);
    }
}