using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class SweetLoot() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AnyEnemy)
{


    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
        
        var stashedCards = (await CardSelectCmd.FromCombatPile(choiceContext, PileType.Draw.GetPile(this.Owner), this.Owner, 
            new CardSelectorPrefs(StashSelectorPrefs.ToStashSelectionPrompt, 1),
            null)).ToList();
        await CardPileCmd.Add(stashedCards, StashCardPile.StashPileType);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}