using BaseLib.Extensions;
using GregTheSpire.GregTheSpireCode.Powers;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace GregTheSpire.GregTheSpireCode.Relics;


public class FannyPack() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Common;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<StoragePower>()
        ];


        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new IntVar("Rounds", 1),
            new PowerVar<StoragePower>(1)
        ];


        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)

        {
            if (player != Owner || player.PlayerCombatState?.TurnNumber > 1) return;
            await PowerCmd.Apply<StoragePower>(
                new ThrowingPlayerChoiceContext(), Owner.Creature,
                DynamicVars.Power<StoragePower>().BaseValue, Owner.Creature, null);
        }

    
}
    