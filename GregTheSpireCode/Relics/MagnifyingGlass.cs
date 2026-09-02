// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Relics.Pendulum
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 73B63EE0-6C0A-47BB-B0D1-B21F6D94222E
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll
// XML documentation location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.xml

using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Entities.Cards;


#nullable enable
namespace GregTheSpire.GregTheSpireCode.Relics;

public class MagnifyingGlass() : GregTheSpireRelic
{
  private const string _turnsKey = "Turns";
  private bool _isActivating;
  private int _turnsSeen;

  public override string FlashSfx => "event:/sfx/ui/relic_activate_draw";

  public override RelicRarity Rarity => RelicRarity.Rare;

  public override bool ShowCounter => true;

  public override int DisplayAmount
  {
    get => !this.IsActivating ? this.TurnsSeen : this.DynamicVars["Turns"].IntValue;
  }

  protected override IEnumerable<DynamicVar> CanonicalVars => [
    new DynamicVar("Turns", 3)
  ];
 

  private bool IsActivating
  {
    get => this._isActivating;
    set
    {
      this.AssertMutable();
      this._isActivating = value;
      this.InvokeDisplayAmountChanged();
    }
  }

  [SavedProperty]
  public int TurnsSeen
  {
    get => this._turnsSeen;
    set
    {
      this.AssertMutable();
      this._turnsSeen = value;
      this.InvokeDisplayAmountChanged();
    }
  }

  public override Task BeforeHandDraw(
    Player player,
    PlayerChoiceContext choiceContext,
    ICombatState combatState)
  {
    if (player != this.Owner)
      return Task.CompletedTask;
    this.TurnsSeen = (this.TurnsSeen + 1) % this.DynamicVars["Turns"].IntValue;
    this.Status = this.TurnsSeen == this.DynamicVars["Turns"].IntValue - 1 ? RelicStatus.Active : RelicStatus.Normal;
    if (this.TurnsSeen == 0)
      TaskHelper.RunSafely(this.DoActivateVisuals());
    return Task.CompletedTask;
  }

  public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
  {
    if (player != this.Owner || this.TurnsSeen != 0)
    {
      return;
    }
    
    await CardPileCmd.ShuffleIfNecessary(choiceContext, player);
    IReadOnlyList<CardPileAddResult> cardPileAddResultList = await CardPileCmd.Add(await CardSelectCmd.FromCombatPile(choiceContext, PileType.Draw.GetPile(player), player, new CardSelectorPrefs(this.SelectionScreenPrompt, 1)), PileType.Draw, CardPilePosition.Top);
  }

  private async Task DoActivateVisuals()
  {
    this.IsActivating = true;
    this.Flash();
    await Cmd.Wait(1f);
    this.IsActivating = false;
  }

  public override Task AfterCombatEnd(CombatRoom _)
  {
    this.Status = RelicStatus.Normal;
    return Task.CompletedTask;
  }
}
