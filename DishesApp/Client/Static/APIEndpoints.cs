namespace Client.Static
{
    public static class APIEndpoints
    {
#if DEBUG
        internal const string ServerBaseUrl = "https://localhost:7024";
#else
        internal const string ServerBaseUrl = "https://localhost:7024";
#endif
        internal readonly static string s_register = $"{ServerBaseUrl}/api/user/register";
        internal readonly static string s_signIn = $"{ServerBaseUrl}/api/user/signin";
        internal readonly static string s_weatherForecast = $"{ServerBaseUrl}/weatherforecast";
        internal readonly static string s_getUsers = $"{ServerBaseUrl}/api/user/get-users";
    }
}
