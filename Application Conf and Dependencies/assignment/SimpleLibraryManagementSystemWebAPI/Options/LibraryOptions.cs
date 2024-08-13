namespace SimpleLibraryManagementSystemWebAPI.Options
{
    public class LibraryOptions
    {
        public const string SettingName = "LibrarySettings";

        public int MaxBorrowedBook { get; set; }

        public int BookLoanDuration { get; set;}
    }
}