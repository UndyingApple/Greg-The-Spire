using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class Ransack() : GregTheSpireCard(3,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.Self)
{

    private int stealAmount = 3;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        StealCmd.StealAsync(choiceContext, this.Owner, this.stealAmount);
    }

    protected override void OnUpgrade()
    {
        stealAmount += 1;
    }
}