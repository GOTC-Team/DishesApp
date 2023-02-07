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
        internal readonly static string s_getUsers = $"{ServerBaseUrl}/api/user/get-users";
        internal readonly static string s_getAllIngredients = $"{ServerBaseUrl}/api/ingredient/get-all-ingredients";
        internal readonly static string s_getUserIngredients = $"{ServerBaseUrl}/api/ingredient/get-user-ingredients";
        internal readonly static string s_addIngredientToUser = $"{ServerBaseUrl}/api/ingredient/add-ingredient-to-user";
        internal readonly static string s_removeIngredientFromUser = $"{ServerBaseUrl}/api/ingredient/remove-ingredient";
    }
}
