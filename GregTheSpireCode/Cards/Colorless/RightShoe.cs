using BaseLib.Utils;
using Godot;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards.Colorless;

[Pool(typeof(TokenCardPool))]
public class RightShoe() : GregTheSpireCard(0,
    CardType.Attack, CardRarity.Token,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move),
        new PowerVar<WeakPower>(1)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromCard<RightShoe>()
    ];
    
    public static async Task<IEnumerable<RightShoe>> CreateInHand(
        Player owner,
        int amount,
        ICombatState combatState,
        Player? creator = null)
    {
        IEnumerable<RightShoe> rightshoes = RightShoe.Create(owner, amount, combatState);
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) rightshoes, PileType.Hand, creator ?? owner);
        IEnumerable<RightShoe> inHand = rightshoes;
        rightshoes = null;
        return inHand;
    }

    public static IEnumerable<RightShoe> Create(Player owner, int amount, ICombatState combatState)
    {
        List<RightShoe> rightshoeList = new List<RightShoe>();
        for (int index = 0; index < amount; ++index)
            rightshoeList.Add(combatState.CreateCard<RightShoe>(owner));
        return (IEnumerable<RightShoe>)rightshoeList;
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "play.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
        WeakPower weakPower = await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, this.DynamicVars.Weak.BaseValue, this.Owner.Creature, (CardModel) this);
        
        IReadOnlyList<CardPileAddResult> cardPileAddResultList = await CardPileCmd.Add((IEnumerable<CardModel>) this.Owner.PlayerCombatState.AllCards.OfType<LeftShoe>().Where<LeftShoe>((Func<LeftShoe, bool>) (c => true)), PileType.Draw, CardPilePosition.Top);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Weak.UpgradeValueBy(1);
    }
}