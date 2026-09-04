using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class GrapplingHook() : GregTheSpireCard(0,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    
    private bool HasBeenPlayedThisTurn
    {
        get
        {
            return CombatManager.Instance.History.CardPlaysFinished.Any<CardPlayFinishedEntry>((Func<CardPlayFinishedEntry, bool>) (e => e.CardPlay.Card == this && e.HappenedThisTurn(this.CombatState)));
        }
    }
    
    protected override bool ShouldGlowGoldInternal => !this.HasBeenPlayedThisTurn;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (this.HasBeenPlayedThisTurn)
            return;
        if (IsUpgraded)
        {
            await StealCmd.StealAsync(choiceContext, this.Owner, 2);
        }
        
        else await StealCmd.StealAsync(choiceContext, this.Owner, 1);
    }


}  