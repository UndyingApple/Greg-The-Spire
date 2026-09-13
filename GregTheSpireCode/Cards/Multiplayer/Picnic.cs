using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards.Multiplayer;

public class Picnic() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AllAllies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new IntVar("snackAmount", 2)
    ];
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
    public override CardMultiplayerConstraint MultiplayerConstraint
    {
        get => CardMultiplayerConstraint.MultiplayerOnly;
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        foreach (Creature creature in (IEnumerable<Creature>) this.CombatState.PlayerCreatures.Where<Creature>((Func<Creature, bool>) (c => c != null && c.IsAlive)).ToList<Creature>())
        {
            for (int index = 0; index < DynamicVars["StealAmount"].IntValue; ++index)
                await CardPileCmd.AddGeneratedCardsToCombat(
                    (IEnumerable<CardModel>)CardFactory
                        .GetDistinctForCombat(creature.Player, SnackTokens, 1, Owner.RunState.Rng.CombatCardGeneration)
                        .ToList<CardModel>(), PileType.Hand, creature.Player);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["snackAmount"].UpgradeValueBy(1);
    }
}