using BaseLib.Abstracts;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

  
public class Thieve() : GregTheSpireCard(2,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self),ITranscendenceCard
{
    public CardModel GetTranscendenceTransformedCard() => ModelDb.Card<SpyMaster>();

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(8M, ValueProp.Move)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        //public static async Task StealAsync(PlayerChoiceContext choiceContext, Player player, int amount, CardModel card_initial)
        await StealCmd.StealAsync(choiceContext, this.Owner, 2);
    }
    
 
    
    


    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4M);
    }
}