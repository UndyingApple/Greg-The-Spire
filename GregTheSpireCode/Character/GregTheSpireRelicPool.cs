using BaseLib.Abstracts;
using GregTheSpire.GregTheSpireCode.Extensions;
using Godot;

namespace GregTheSpire.GregTheSpireCode.Character;

public class GregTheSpireRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => GregTheSpire.Color;

    public override string BigEnergyIconPath => "charui/greg_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/small_greg_energy.png".ImagePath();
}