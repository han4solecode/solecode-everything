namespace VehiclesSystemAPI.Models
{
    public class BlogOptions
    {
        public const string SettingName = "BlogSettings";

        public string? Prefix { get; set; }

        public int PostsPerPage { get; set; }
    }
}
