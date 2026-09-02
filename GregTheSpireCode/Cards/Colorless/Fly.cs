using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using GregTheSpire.GregTheSpireCode.Tags;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

[Pool(typeof(TokenCardPool))]
public class Fly() : GregTheSpireCard(0,
    CardType.Attack, CardRarity.Token,
    TargetType.TargetedNoCreature)
{
    private bool retained = false;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain,
        CardKeyword.Exhaust,
        GregTheSpireKeywords.Snack
    ];
    

    protected override HashSet<CardTag> CanonicalTags
    {
        get => new HashSet<CardTag>() { GregTheSpireTags.Fly };
    }
    
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar("Played", 3, ValueProp.Move),
        new DamageVar("Retained", 2, ValueProp.Move)
    ];

    public static async Task<IEnumerable<Fly>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Fly> flies = Fly.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) flies, PileType.Hand, creator ?? owner);
        IEnumerable<Fly> inHand = flies;
        flies = null;
        return inHand;
    }
    
    public static IEnumerable<Fly> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Fly> flyList = new List<Fly>();
        for (int index = 0; index < amount; ++index)
            flyList.Add(combatState.CreateCard<Fly>(owner));
        return (IEnumerable<Fly>) flyList;
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars["Played"].BaseValue)
            .FromCard((CardModel)this, play).TargetingAllOpponents(this.CombatState).Execute(choiceContext);
    }

    public override async Task AfterFlush(PlayerChoiceContext choiceContext, Player player, IReadOnlyCollection<CardModel> flushedCards,
        IReadOnlyCollection<CardModel> retainedCards)
    {
        if (retainedCards.Contains(this)) await DamageCmd.Attack(this.DynamicVars["Retained"].BaseValue).FromCard((CardModel) this, null).TargetingRandomOpponents(this.CombatState).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Played"].UpgradeValueBy(1);
        DynamicVars["Retained"].UpgradeValueBy(1);
    }
}