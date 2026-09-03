using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    new BlockVar(3, ValueProp.Move)
    ];

    public override bool GainsBlock => true;
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //i know this'll probably be changed, so here's some documentation:
        /*
         takes stuff from Fiend Fire to make a list of TitleLocStrings
         (term for just the base card title without any flair from upgrades or such, 
         actual type is LocString and TitleLocString just returns the LocString associated with its title)
         and traverse the Exhaust pile to add in all of the cards with Snack whose titles
         are not yet in the list.
         */
        List<CardModel> list = PileType.Exhaust.GetPile(this.Owner).Cards.ToList<CardModel>();
        List<LocString> uniqueSnacks = null;
        foreach (CardModel card in list)
        {
            if (!uniqueSnacks.Contains(card.TitleLocString) && card.Keywords.Contains(GregTheSpireKeywords.Snack))
            {
                uniqueSnacks.Add(card.TitleLocString);
            }
        }

        //Multiplies the base amount by the number of unique snacks counted earlier + 1 to create the actual block that'll be applied, totalBlock
        //this ensures that the base amount is always included, and that any Snacks in the exhaust will cause an increase in addition to that base
        //this variable is what you'll need to retool for card localization
        BlockVar totalBlock = new BlockVar(DynamicVars.Block.BaseValue * (uniqueSnacks.Count + 1), ValueProp.Move);
        
        await CreatureCmd.GainBlock(Owner.Creature, totalBlock, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(1);
    }
}