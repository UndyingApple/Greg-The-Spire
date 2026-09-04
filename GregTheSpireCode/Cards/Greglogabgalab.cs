using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Greglogabgalab() : GregTheSpireCard(3,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, 1, this.Owner);
        await Cmd.Wait(0.1f);
        
        CardModel stashedCard =
            this.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>(
                (IEnumerable<CardModel>)PileType.Hand.GetPile(this.Owner).Cards);
        if (stashedCard != null) await StashCmd.StashAsync(choiceContext, this.Owner, stashedCard);
        await Cmd.Wait(0.1f);
        
        CardModel autoPlayedCard =
            this.Owner.RunState.Rng.CombatCardSelection.NextItem<CardModel>(
                (IEnumerable<CardModel>)PileType.Hand.GetPile(this.Owner).Cards);
        if (autoPlayedCard != null) await CardCmd.AutoPlay(choiceContext, autoPlayedCard, null);
        await Cmd.Wait(0.1f);
        
        await PlayRandFromStashCmd.PlayRandFromStashCmdAsync(choiceContext, this.Owner, 1);
        await Cmd.Wait(0.1f);
        
        await StealCmd.StealAsync(choiceContext, this.Owner, 1);
        await Cmd.Wait(0.1f);
        
        //PlayerCmd.EndTurn(Owner, false);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}