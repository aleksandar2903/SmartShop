namespace SmartShop
{
    public static class Config
    {
        static readonly string BaseAddress = "http://192.168.1.103:8000";
        public static readonly string APIUrl = $"{BaseAddress}/api/";
        public static readonly string StorageAddress = $"{BaseAddress}/storage/images/";
    }
}
