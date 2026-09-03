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

public class Gourmand() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(4, ValueProp.Move)
    ];
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        //i know this'll probably be changed, so here's some documentation so you don't have to remake this from scratch:
        /*
         takes stuff from Fiend Fire to make a list of TitleLocStrings
         (TitleLocString is just a term for the base card title without any flair from upgrades or such,
         actual type of it is LocString and the TitleLocString method just returns the LocString associated with a card's title)
         and traverse the Exhaust pile to add in all of the cards with the Snack keyword whose titles
         are not yet in the list of TitleLocStrings.
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

        //Multiplies the base amount by the number of unique snacks counted earlier + 1 to create the actual damage that'll be applied, totalDamage
        //this ensures that the base amount is always included, and that any Snacks in the exhaust will cause an increase in addition to that base
        //this variable is what you'll need to retool for card localization
        DamageVar totalDamage = new DamageVar(DynamicVars.Damage.BaseValue * (uniqueSnacks.Count + 1), ValueProp.Move);
        
        await DamageCmd.Attack(totalDamage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}