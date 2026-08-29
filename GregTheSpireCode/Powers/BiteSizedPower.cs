using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace GregTheSpire.GregTheSpireCode.Powers;

public class BiteSizedPower() : GregTheSpirePower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;
    
}