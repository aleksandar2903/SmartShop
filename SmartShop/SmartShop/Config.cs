namespace SmartShop
{
    public static class Config
    {
        static readonly string BaseAddress = "http://10.0.2.2:8000";
        public static readonly string APIUrl = $"{BaseAddress}/api";
        public static readonly string StorageAddress = $"{BaseAddress}/storage/images/";
    }
    public static class StateKeys
    {
        public const string Offline = "Offline";
        public const string EmptyQuery = "EmptyQuery";
    }
}
