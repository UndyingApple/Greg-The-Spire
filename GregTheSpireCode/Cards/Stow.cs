using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Stow() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        
        var stashedCards = (await CardSelectCmd.FromHand(choiceContext, Owner, 
            new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, 0, 2),
            null,
            this)).ToList();
        
        await CardPileCmd.Add(stashedCards, StashCardPile.StashPileType);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}