using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;


namespace GregTheSpire.GregTheSpireCode.Cards;


public class ExtraPockets() : GregTheSpireCard(1,
   CardType.Power, CardRarity.Uncommon,
   TargetType.Self)
{
   protected override IEnumerable<DynamicVar> CanonicalVars => [
       new PowerVar<StoragePower>(2)
   ];


   protected override async Task OnPlay(
       PlayerChoiceContext choiceContext,
       CardPlay play)
   {
       await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
       StoragePower StoragePower = await PowerCmd.Apply<StoragePower>(choiceContext, this.Owner.Creature, this.DynamicVars["StoragePower"].BaseValue, this.Owner.Creature, (CardModel) this);
   }
  
protected override void OnUpgrade() => this.AddKeyword(CardKeyword.Innate);
}
