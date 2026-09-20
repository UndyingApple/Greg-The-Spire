using BaseLib.Config;

namespace GregTheSpire.GregTheSpireCode.Data;

public class GregTheSpireConfig : SimpleModConfig
{
    [ConfigHoverTip]
    public static bool UploadMetrics { get; set; } = false;

    [ConfigHideInUI]
    [ConfigIgnoreRestoreDefaults]
    public static bool UploadMetricsFtueSeen { get; set; } = false;
}