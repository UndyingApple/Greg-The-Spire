// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Potions.SwiftPotion
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 73B63EE0-6C0A-47BB-B0D1-B21F6D94222E
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll
// XML documentation location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.xml

#nullable enable


using BaseLib.Utils;
using Godot;
using GregTheSpire.GregTheSpireCode.Character;
using GregTheSpire.GregTheSpireCode.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace GregTheSpire.GregTheSpireCode.Potions;


[Pool(typeof(GregTheSpirePotionPool))]

public sealed class StealPoition : GregTheSpirePotion
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.AnyPlayer;


    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        GregTheSpirePotion.AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, new Color("45e6d0"));
        await StealCmd.StealAsync(choiceContext, target.Player, 2);
    }
}