using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Cards;
using GregTheSpire.GregTheSpireCode.Cards.Colorless;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace GregTheSpire.GregTheSpireCode.Cards;


public class FlySwatter() : GregTheSpireCard(1,
    CardType.Power, CardRarity.Uncommon,
    TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
         new PowerVar<FlySwatterPower>(3)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Fly>()
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "PowerUp", this.Owner.Character.PowerUpAnimDelay);
        FlySwatterPower flyswatterPower = await PowerCmd.Apply<FlySwatterPower>(choiceContext, this.Owner.Creature,this.DynamicVars["FlySwatterPower"].BaseValue, this.Owner.Creature, (CardModel) this);
        
        IEnumerable<Fly> cards = Fly.Create(this.Owner, 1, this.CombatState);
        await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>)cards, PileType.Hand, this.Owner,
            CardPilePosition.Top);
    }
    

    protected override void OnUpgrade()
    { 
        DynamicVars.Power<FlySwatterPower>().UpgradeValueBy(2);
    }
}