using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Potions;

[Pool(typeof(GregTheSpirePotionPool))]

public sealed class SnackPotion : GregTheSpirePotion
{

    public override PotionRarity Rarity => PotionRarity.Common;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;

    private static IReadOnlyList<CardModel> SnackTokens =>
    [
        ModelDb.Card<Strawberry>(),
        ModelDb.Card<Soda>(),
        ModelDb.Card<Olive>(),
        ModelDb.Card<Cheese>(),
        ModelDb.Card<Cracker>(),
        ModelDb.Card<Fly>(),
        ModelDb.Card<Soup>()
    ];


    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        GregTheSpirePotion.AssertValidForTargetedPotion(target);
        var player = target.Player;

        if (player != null)
        {
            for (var i = 0; i < 2; ++i)
            {
                CardModel card = await CardSelectCmd.FromChooseACardScreen(choiceContext, (IReadOnlyList<CardModel>) CardFactory.GetDistinctForCombat(player, SnackTokens, 3, player.RunState.Rng.CombatCardGeneration).ToList<CardModel>(), player, true);
                if (card != null)
                {
                    var combat =
                        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, this.Owner);
                }
                
            }
        }
    }
}
