using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class RatatoingingIt() : GregTheSpireCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [

        new IntVar("Replay", 1)
    ];
    
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
      
        await PowerCmd.Apply<NoSkillsPlayed>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            foreach (CardModel allCard in this.Owner.PlayerCombatState.AllCards)
            {
                allCard.BaseReplayCount += this.DynamicVars["Replay"].IntValue;
            }
    }



    protected override void OnUpgrade()
    {
        this.DynamicVars["Replay"].UpgradeValueBy(1);
    }
}