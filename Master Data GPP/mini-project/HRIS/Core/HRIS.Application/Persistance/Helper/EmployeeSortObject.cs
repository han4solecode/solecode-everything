namespace HRIS.Application.Persistance.Helper
{
    public class EmployeeSortObject
    {
        public string? SortBy { get; set; }

        public bool IsDescending { get; set; } = false;
    }
}