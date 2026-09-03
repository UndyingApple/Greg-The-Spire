using System.Diagnostics;
using BaseLib.Utils;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Salivate() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Common,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    new BlockVar(3, ValueProp.Move),
    new CalculationBaseVar(3),
    new CalculationExtraVar(3),
    (DynamicVar) new CalculatedBlockVar(ValueProp.Move).WithMultiplier(
        (Func<CardModel, Creature, Decimal>) ((card, _) => (Decimal) PileType.Exhaust.GetPile(card.Owner).Cards.DistinctBy<CardModel, String>((Func<CardModel, String>) (c => c.Id.Entry)).Count<CardModel>((Func<CardModel, bool>)(c => c.Keywords.Contains(GregTheSpireKeywords.Snack)))))
                ];//so, this is a monstrosity. Here's what it does: gets the card owner's exhaust pile, then narrows it down into a subset of cards with distinct Ids (effectively their titles) and returns a decimal count of the number of unique cards in the exhaust pile with the custom Keyword "Snack", 

    public override bool GainsBlock => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //Original Code that works, but is inconducive to localization:
        /*
         takes stuff from Fiend Fire to make a list of TitleLocStrings
         (term for just the base card title without any flair from upgrades or such, 
         actual type is LocString and TitleLocString just returns the LocString associated with its title)
         and traverse the Exhaust pile to add in all of the cards with Snack whose titles
         are not yet in the list.
         
        List<CardModel> exhaust = PileType.Exhaust.GetPile(this.Owner).Cards.ToList<CardModel>();
        foreach (CardModel card in exhaust.ToList())
        {
            if (!card.Keywords.Contains(GregTheSpireKeywords.Snack))
            {
                exhaust.Remove(card);
            }
        }
        
        
        //Multiplies the base amount by the number of unique snacks counted earlier + 1 to create the actual block that'll be applied, totalBlock
        //this ensures that the base amount is always included, and that any Snacks in the exhaust will cause an increase in addition to that base
        //this variable is what you'll need to retool for card localization
        BlockVar totalBlock = new BlockVar(DynamicVars.Block.BaseValue * (exhaust.Select(c => c.Id.Entry).Distinct().Count() + 1), ValueProp.Move);
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.GainBlock(Owner.Creature, totalBlock, play);
        */
        
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.GainBlock(this.Owner.Creature, this.DynamicVars.CalculatedBlock.Calculate(play.Target), this.DynamicVars.CalculatedBlock.Props, play);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(1);
        DynamicVars.CalculationExtra.UpgradeValueBy(1);
    }
}