using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class CheeseClubPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Cheese>()
    ];

    public async Task AfterPlayerTurnStart(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner.Player || player.PlayerCombatState.AllCards.OfType<CheeseBall>().Where<CheeseBall>(
                (Func<CheeseBall, bool>)(c =>
                {
                    CardPile pile = c.Pile;
                    return pile != null && pile.Type == PileType.Hand;
                })).ToList<CheeseBall>().Count > 0)
            return;
        IEnumerable<CardModel> inHand = await CheeseBall.CreateInHand(this.Owner.Player, this.Amount, combatState);
    }
}