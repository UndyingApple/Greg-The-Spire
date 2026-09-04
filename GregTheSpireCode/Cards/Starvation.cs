using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Enchantments;
using GregTheSpire.GregTheSpireCode.Keywords;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Starvation() : GregTheSpireCard(1,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("numCards", 1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {

      

        if (IsUpgraded)
        {
            foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, Owner, 
                new CardSelectorPrefs(SnackSelectorPrefs.SnackSelectionPrompt, 3),
                (Func<CardModel, bool>) (c => !c.GetKeywordsWithSources(KeywordSources.Local).Contains(GregTheSpireKeywords.Snack)),
                this))
            {
                CardCmd.ApplyKeyword(card, GregTheSpireKeywords.Snack);
            }
        }
        else
        {
            foreach (CardModel card in await CardSelectCmd.FromHand(choiceContext, Owner, 
                         new CardSelectorPrefs(SnackSelectorPrefs.SnackSelectionPrompt, 2),
                         (Func<CardModel, bool>) (c => !c.GetKeywordsWithSources(KeywordSources.Local).Contains(GregTheSpireKeywords.Snack)),
                         this))
            {
                CardCmd.ApplyKeyword(card, GregTheSpireKeywords.Snack);
            }
        }
    }
    
    public struct SnackSelectorPrefs {
        public static LocString SnackSelectionPrompt => new LocString("card_selection", "GREGTHESPIRE-APPLY_SNACK");
    }
    protected override void OnUpgrade()
    {
        DynamicVars["numCards"].UpgradeValueBy(1);
    }
}