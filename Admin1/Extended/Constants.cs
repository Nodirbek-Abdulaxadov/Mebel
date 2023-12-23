namespace Admin1.Extended;

public static class Constants
{
    #if DEBUG
        public const string BASE_URL = "https://localhost:44304/api";
    #else
        public const string BASE_URL = "https://mebel-api.1kb.uz/api";
    #endif
}