using System.Text.Json;

namespace GregTheSpire.GregTheSpireCode.Data;

internal static class GregTheSpireMetricsTest
{
    public static async Task SendTest()
    {
        var payload = new
        {
            mod_version = "TEST",
            has_foreign_content = false,
            data = new
            {
                test = true,
                message = "Testing :)"
            }
        };

        string json = JsonSerializer.Serialize(payload);

        await GregTheSpireMetricsClient.Upload(json);
    }
}