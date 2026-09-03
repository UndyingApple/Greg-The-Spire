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

public class RatatoingingIt() : GregTheSpireCard(1,
    CardType.Attack, CardRarity.Rare,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(1, ValueProp.Move),
        new IntVar("Replay", 1)
    ];

    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    
    

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).Targeting(play.Target).Execute(choiceContext);
        await PowerCmd.Apply<NoSkillsPlayed>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            foreach (CardModel allCard in this.Owner.PlayerCombatState.AllCards)
            {
                if (allCard is not RatatoingingIt)
                    allCard.BaseReplayCount += this.DynamicVars["Replay"].IntValue;
            }
    }



    protected override void OnUpgrade()
    {
        this.DynamicVars["Replay"].UpgradeValueBy(1);
    }
}