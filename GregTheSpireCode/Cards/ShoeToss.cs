using Godot;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class ShoeToss() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move),
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<LeftShoe>(IsUpgraded),
        HoverTipFactory.FromCard<RightShoe>(IsUpgraded)
    ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        ArgumentNullException.ThrowIfNull((object) play.Target, "play.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
        
        IEnumerable<CardModel> leftshoe = await LeftShoe.CreateInHand(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in leftshoe)
            {
                CardCmd.Upgrade(card);
            }
        }
        await Cmd.Wait(0.1f);
        IEnumerable<CardModel> rightshoe = await RightShoe.CreateInHand(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in rightshoe)
            {
                CardCmd.Upgrade(card);
            }
        }
    }

    protected override void OnUpgrade()
    {
        
    }
}