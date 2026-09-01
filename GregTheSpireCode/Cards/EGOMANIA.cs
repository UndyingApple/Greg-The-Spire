using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;


namespace GregTheSpire.GregTheSpireCode.Cards;


public class EGOMANIA() : GregTheSpireCard(2,
   CardType.Power, CardRarity.Ancient,
   TargetType.Self)
{
   protected override IEnumerable<DynamicVar> CanonicalVars => [];


   protected override async Task OnPlay(
       PlayerChoiceContext choiceContext,
       CardPlay play)
   {
       await PowerCmd.Apply<ConfidencePower>(choiceContext, Owner.Creature,
                   8, Owner.Creature, this);
      
       await PowerCmd.Apply<EGOMANIAPower>(choiceContext, Owner.Creature,
           4, Owner.Creature, this);
   }


   protected override void OnUpgrade()
   {
       this.EnergyCost.UpgradeBy(-1);
   }
}
