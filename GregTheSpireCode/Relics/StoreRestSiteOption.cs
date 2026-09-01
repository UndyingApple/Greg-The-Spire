using Godot;
using GregTheSpire.GregTheSpireCode.Enchantments;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rewards;

namespace GregTheSpire.GregTheSpireCode.Relics;

public class StoreRestSiteOption(Player owner) : RestSiteOption(owner)
{
  private IEnumerable<CardModel>? _selection;

  public override string OptionId => "STORE";


  public int StoreCount { get; set; } = 1;

   public override LocString Description
   {
     get
     {
       if (!this.IsEnabled)
         return new LocString("rest_site_ui", $"OPTION_{this.OptionId}.descriptionDisabled");
       LocString description = new LocString("rest_site_ui", $"OPTION_{this.OptionId}.description");
       description.Add("Cards", 1);
       return description;
     }
   }
   public override bool IsEnabled => 3 >= 2;

   public override async Task<bool> OnSelect()
   {
     CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
     IEnumerable<CardModel> source = await CardSelectCmd.FromDeckForEnchantment(this.Owner,(EnchantmentModel) ModelDb.Enchantment<Stowaway>(), 1, prefs);
     
     if (!source.Any<CardModel>())
       return false;
     foreach (CardModel card in source)
        CardCmd.Enchant<Stowaway>(card, 1);
    
     return true;
   }


}