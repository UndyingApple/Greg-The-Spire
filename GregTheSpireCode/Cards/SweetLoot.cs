using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class SweetLoot() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(6, ValueProp.Move)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        
        var stashedCards = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Draw.GetPile(this.Owner), this.Owner, 
            new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, 1),
            null)).ToList();
        await CardPileCmd.Add(stashedCards, StashCardPile.StashPileType);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}