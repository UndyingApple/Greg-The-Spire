using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

[Pool(typeof(TokenCardPool))]
public class Cracker() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        GregTheSpireKeywords.Snack,
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(3, ValueProp.Move)
    ];
    
    public static async Task<IEnumerable<Cracker>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<Cracker> crackers = Cracker.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) crackers, PileType.Hand, creator ?? owner);
        IEnumerable<Cracker> inHand = crackers;
        crackers = (IEnumerable<Cracker>) null;
        return inHand;
    }
    
    public static IEnumerable<Cracker> Create(Player owner, int amount, ICombatState combatState)
    {
        List<Cracker> crackerList = new List<Cracker>();
        for (int index = 0; index < amount; ++index)
            crackerList.Add(combatState.CreateCard<Cracker>(owner));
        return (IEnumerable<Cracker>) crackerList;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
    }
}