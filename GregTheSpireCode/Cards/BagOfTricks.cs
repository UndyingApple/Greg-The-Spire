using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class BagOfTricks() : GregTheSpireCard(
	0,
	CardType.Skill, CardRarity.Basic,
	TargetType.Self)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new CardsVar(1)
	];

	protected override async Task OnPlay(
		PlayerChoiceContext choiceContext,
		CardPlay play)
	{
		await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
		if (IsUpgraded)
		{
			await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
		}
		
		await PlayFromStashCmd.PlayFromStashCmdAsync(choiceContext, this.Owner, 1, 1,null);
		
		var stashedCards = (await CardSelectCmd.FromHand(choiceContext, Owner, 
			new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, 1, DynamicVars.Cards.IntValue),
			null,
			this)).ToList();
		
		await CardPileCmd.Add(stashedCards, StashCardPile.StashPileType);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Cards.UpgradeValueBy(1);
	}
}
