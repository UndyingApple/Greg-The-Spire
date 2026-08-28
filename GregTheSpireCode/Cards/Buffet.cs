using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Buffet() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Cheese>(IsUpgraded),
        HoverTipFactory.FromCard<Cracker>(IsUpgraded),
        HoverTipFactory.FromCard<Fly>(IsUpgraded)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        IEnumerable<CardModel> cheese = await Cheese.CreateInHand(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in cheese)
            {
                CardCmd.Upgrade(card);
            }
        }
        await Cmd.Wait(0.1f);
        IEnumerable<CardModel> cracker = await Cracker.CreateInHand(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in cracker)
            {
                CardCmd.Upgrade(card);
            }
        }
        await Cmd.Wait(0.1f);
        IEnumerable<CardModel> fly = await Fly.CreateInHand(Owner, 1, CombatState);
        if (IsUpgraded)
        {
            foreach (CardModel card in fly)
            {
                CardCmd.Upgrade(card);
            }
        }
    }
}