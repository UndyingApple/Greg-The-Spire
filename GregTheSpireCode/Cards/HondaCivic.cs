using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class HondaCivic() : GregTheSpireCard(2,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        //(DynamicVar) new DamageVar(7 + 2 * CombatManager.Instance.History.CardPlaysStarted.Count(e => e.CardPlay.Player == Owner && e.CardPlay.Card.Keywords.Contains(GregTheSpireKeywords.Snack)), ValueProp.Move)
        new CalculationBaseVar(26),
        new ExtraDamageVar(-3),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((Func<CardModel, Creature, Decimal>) ((card, _) => (Decimal) card.CombatState.HittableEnemies.Count))    
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.CalculatedDamage).FromCard((CardModel)this, play).TargetingAllOpponents(this.CombatState).Execute(choiceContext);    
    }

    protected override void OnUpgrade()
    {
        DynamicVars.CalculationBase.UpgradeValueBy(1);
    } 
}