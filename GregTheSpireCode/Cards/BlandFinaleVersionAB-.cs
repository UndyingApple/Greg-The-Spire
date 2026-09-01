using Godot;
using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace GregTheSpire.GregTheSpireCode.Cards;

public class BlandFinaleVersionAB() : GregTheSpireCard(0,
    CardType.Attack, CardRarity.Uncommon,
    TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(20M, ValueProp.Move)];

    
    protected override bool ShouldGlowGoldInternal => this.IsPlayable;
    
    protected override bool IsPlayable => StashCardPile.StashPileType.GetPile(this.Owner).Cards.Count == 0 && PileType.Draw.GetPile(this.Owner).Cards.Count <= 4;
    
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        NGrandFinaleVfx child = NGrandFinaleVfx.Create(this.Owner.Creature);
        if (child != null)
        {
            NCombatRoom instance = NCombatRoom.Instance;
            if (instance != null)
                instance.CombatVfxContainer.AddChildSafely((Node) child);
            await Cmd.Wait(NGrandFinaleVfx.totalAnticipationDuration);
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        AttackCommand attackCommand = await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue).FromCard((CardModel) this, play).TargetingAllOpponents(this.CombatState).WithHitFx(tmpSfx: "blunt_attack.mp3").Execute(choiceContext);

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(10);
    }
}