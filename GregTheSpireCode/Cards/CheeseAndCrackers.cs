using BaseLib.Abstracts;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class CheeseAndCrackers() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new IntVar("CrackerAmt", 1)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Cheese>(IsUpgraded),
        HoverTipFactory.FromCard<Cracker>(IsUpgraded)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        IEnumerable<CardModel> cheese = await Cheese.CreateInHand(Owner, 1, CombatState);
        
        await Cmd.Wait(0.1f);
        await Cracker.CreateInHand(Owner,  DynamicVars["CrackerAmt"].IntValue, CombatState);

    }

    protected override void OnUpgrade()
    {
        DynamicVars["CrackerAmt"].UpgradeValueBy(1);
    }
}