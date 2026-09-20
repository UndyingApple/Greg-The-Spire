using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class DarkenedChocolate() : GregTheSpireCard(10,
    CardType.Skill, CardRarity.Rare,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(2),
        new IntVar("CardsStolen", 0)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WeakPower>(),
        HoverTipFactory.FromPower<VulnerablePower>(),
        HoverTipFactory.FromKeyword(GregTheSpireKeywords.Steal),
        HoverTipFactory.FromKeyword(GregTheSpireKeywords.Stash)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState == null) return;
        foreach (Creature enemy in CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, enemy, 3, this.Owner.Creature, this);
            await PowerCmd.Apply<VulnerablePower>(choiceContext, enemy, 3, this.Owner.Creature, this);
        }
    }
    
    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this || this.IsClone)
            return Task.CompletedTask;
        this.EnergyCost.AddThisCombat(-Stolen.NumStolenCombat.Get(card.Owner) * this.DynamicVars.Energy.IntValue);
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int numStolen = Stolen.NumStolenCombat.Get(cardPlay.Player);
        if (cardPlay.Player == this.Owner && numStolen != DynamicVars["CardsStolen"].BaseValue)
        {
            EnergyCost.AddThisCombat(-DynamicVars.Energy.IntValue);
            DynamicVars["CardsStolen"].BaseValue = numStolen;
        }
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-2);
}