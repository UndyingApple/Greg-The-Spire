using GregTheSpire.GregTheSpireCode.Powers;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace GregTheSpire.GregTheSpireCode.Powers;

 

public class StoragePower() : GregTheSpirePower 
{
    public override PowerType Type =>
        PowerType.Buff;
    

    public override PowerStackType StackType =>
        PowerStackType.Counter;
    
    
    public override bool AllowNegative => false;
    
}