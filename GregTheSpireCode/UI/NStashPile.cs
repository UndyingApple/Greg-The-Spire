using BaseLib.Utils;
using Godot;
using GregTheSpire.GregTheSpireCode.CardPiles;
using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.ControllerInput;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.HoverTips;

namespace GregTheSpire.GregTheSpireCode.ui;

public partial class NStashPile : NCombatCardPile
{
	private Player? _player;
	private ComboControllerIcons? _comboIcons;
	private GregTheSpireMegaLabel _storageLabel;
	private int _currentStorage;
	private const float HideOffsetX = -150f;
	protected override PileType Pile => StashCardPile.StashPileType;
	private static readonly string _scenePath = GregTheSpireResources.StashPileScene;
	
	/*
	 * 
	 */
	public static AddedNode<NCombatPilesContainer, NStashPile> _ = new(container =>
	{
		var stashPileButton = ResourceLoader.Load<PackedScene>(_scenePath).Instantiate<NStashPile>();
		stashPileButton.Name = "%StashPile";
		stashPileButton.Position = new Vector2(35, 700);
		
		var background = stashPileButton.GetNode<TextureRect>("CountContainer/Background");
		background.Texture = ResourceLoader.Load<Texture2D>("res://images/packed/combat_ui/pile_button_count.png");

		var countLabel = stashPileButton.GetNode<GregTheSpireMegaLabel>("CountContainer/Count");
		var font = PreloadManager.Cache.GetAsset<Font>(GregTheSpireResources.MegaLabelFont);
		countLabel.AddThemeFontOverride(ThemeConstants.Label.Font, font);
		countLabel.MinFontSize = 20;
		countLabel.MaxFontSize = 26;
		
		var background2 = stashPileButton.GetNode<TextureRect>("CountContainer2/Background");
		background2.Texture = ResourceLoader.Load<Texture2D>("res://images/packed/combat_ui/pile_button_count.png");

		var storageLabel = stashPileButton.GetNode<GregTheSpireMegaLabel>("CountContainer2/Count");
		storageLabel.AddThemeFontOverride(ThemeConstants.Label.Font, font);
		storageLabel.MinFontSize = 20;
		storageLabel.MaxFontSize = 26;

		return stashPileButton;
	});
	
	/*
	 * Activates when the node is added to the scene tree
	 */
	public override void _Ready()
	{
		ConnectSignals();
		_emptyPileMessage = new LocString("combat_messages", "OPEN_EMPTY_STASH");
		/*
		 Screw controller support until we can actually get it working
		_comboIcons = new ComboControllerIcons(
			GetNode<TextureRect>("%ControllerIcon2"), // LT
			GetNode<TextureRect>("%ControllerIcon"), // RT
			MegaInput.viewDrawPile,
			MegaInput.viewDiscardPile, 
			GetNode<GregTheSpireMegaLabel>("%AddSymbol"));
		_comboIcons.Refresh();
		*/
		SetAnimInOutPositions();
	}
	
	protected override void ConnectSignals()
	{
		base.ConnectSignals();
		this._storageLabel = this.GetNode<GregTheSpireMegaLabel>((NodePath) "CountContainer2/Count");
	}
	
	/*
	 * Initializing on its own is pretty much fine, declares basic local variables
	 */
	public override void Initialize(Player player)
	{
		base.Initialize(player);
		_currentStorage = player.Creature.GetPowerAmount<StoragePower>();
		_storageLabel.SetTextAutoSize(_currentStorage.ToString());
	}
	
	protected override void SetAnimInOutPositions()
	{
		_showPosition = Position;
		_hidePosition = Position + new Vector2(HideOffsetX, 0f);
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		if (NControllerManager.Instance != null)
		{
			NControllerManager.Instance.ControllerDetected += OnControllerChanged;
			NControllerManager.Instance.MouseDetected += OnControllerChanged;
			NControllerManager.Instance.ControllerTypeChanged += OnControllerChanged;
			StoragePower.StorageChanged += OnStorageChanged;
		}

		if (NInputManager.Instance != null)
			NInputManager.Instance.InputRebound += OnControllerChanged;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		if (NControllerManager.Instance != null)
		{
			NControllerManager.Instance.ControllerDetected -= OnControllerChanged;
			NControllerManager.Instance.MouseDetected -= OnControllerChanged;
			NControllerManager.Instance.ControllerTypeChanged -= OnControllerChanged;
			StoragePower.StorageChanged -= OnStorageChanged;
		}

		if (NInputManager.Instance != null)
			NInputManager.Instance.InputRebound -= OnControllerChanged;
	}
	
	private void OnStorageChanged(PlayerCombatState pcs, int newVal)
	{
		_currentStorage = newVal;
		_storageLabel.Text = _currentStorage.ToString();
	}

	private void OnControllerChanged() => _comboIcons?.Refresh();
}
