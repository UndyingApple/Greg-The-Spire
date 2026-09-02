using GregTheSpire.GregTheSpireCode.Commands;
using GregTheSpire.GregTheSpireCode.Relics;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace GregTheSpire.GregTheSpireCode.Relics;


public class BoneSaw() : GregTheSpireRelic
{
    public override RelicRarity Rarity =>
        RelicRarity.Uncommon;

    
    
      public override async Task AfterDeath(
        PlayerChoiceContext choiceContext,
        Creature target,
        bool wasRemovalPrevented,
        float deathAnimLength)
      {
        if (target.Side == this.Owner.Creature.Side)
          return;
        this.Flash();
        await PlayFromStashCmd.PlayFromStashCmdAsync(choiceContext, this.Owner, 1, 1,null);
      }
}