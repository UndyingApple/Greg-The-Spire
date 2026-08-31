using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

[Pool(typeof(TokenCardPool))]
public class Strawberry() : GregTheSpireCard(0,
    CardType.Attack, CardRarity.Token,
    TargetType.RandomEnemy)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        GregTheSpireKeywords.Snack,
        CardKeyword.Exhaust
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move),
        new BlockVar(4, ValueProp.Move)
    ];
    
    public static async Task<IEnumerable<Strawberry>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Strawberry> strawberries = Strawberry.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) strawberries, PileType.Hand, creator ?? owner);
        IEnumerable<Strawberry> inHand = strawberries;
        strawberries = null;
        return inHand;
    }
    
    public static IEnumerable<Strawberry> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Strawberry> strawberryList = new List<Strawberry>();
        for (int index = 0; index < amount; ++index)
            strawberryList.Add(combatState.CreateCard<Strawberry>(owner));
        return (IEnumerable<Strawberry>) strawberryList;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, null).TargetingRandomOpponents(this.CombatState).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
        DynamicVars.Block.UpgradeValueBy(2);
    }
}