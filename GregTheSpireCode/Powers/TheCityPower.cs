using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class TheCityPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public IEnumerable<CardModel> SnackTokens =>
    [
        ModelDb.Card<Strawberry>(),
        ModelDb.Card<Soda>(),
        ModelDb.Card<Olive>(),
        ModelDb.Card<Cheese>(),
        ModelDb.Card<Cracker>(),
        ModelDb.Card<Fly>(),
        ModelDb.Card<Soup>()
    ];
    
    public override async Task BeforeHandDraw(
        Player player,
        PlayerChoiceContext choiceContext,
        ICombatState combatState)
    {
        if (player != this.Owner.Player)
            return;
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) CardFactory.GetDistinctForCombat(player, SnackTokens, this.Amount, player.RunState.Rng.CombatCardGeneration).ToList<CardModel>(), PileType.Hand, this.Owner.Player);
        this.Flash();
    }
}
    