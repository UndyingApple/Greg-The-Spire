using System.Net.Http;
using System.Text;

namespace GregTheSpire.GregTheSpireCode.Data;

internal static class GregTheSpireMetricsClient
{
    private static readonly HttpClient HttpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(15)
    };

    public static async Task<bool> Upload(string json)
    {
        try
        {
            using HttpRequestMessage request = new(
                HttpMethod.Post,
                GregTheSpireMetricsEndpoint.RunsUrl);

            request.Headers.Add(
                "apikey",
                GregTheSpireMetricsEndpoint.PublishableKey);

            request.Headers.Add(
                "Prefer",
                "return=minimal");

            request.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response =
                await HttpClient.SendAsync(request);

            string responseBody =
                await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                MainFile.Logger.Info(
                    $"[GregTheSpire] Metrics upload succeeded: " +
                    $"{(int)response.StatusCode} {response.StatusCode}");

                return true;
            }

            MainFile.Logger.Info(
                $"[GregTheSpire] Metrics upload FAILED: " +
                $"{(int)response.StatusCode} {response.StatusCode}\n" +
                responseBody);

            return false;
        }
        catch (Exception ex)
        {
            MainFile.Logger.Info(
                $"[GregTheSpire] Metrics upload exception:\n{ex}");

            return false;
        }
    }
}