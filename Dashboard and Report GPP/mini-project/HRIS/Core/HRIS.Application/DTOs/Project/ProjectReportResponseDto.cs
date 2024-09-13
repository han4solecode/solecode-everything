namespace HRIS.Application.DTOs.Project
{
    public class ProjectReportResponseDto
    {
        public int? ProjectNo { get; set; }

        public string? ProjectName { get; set; }

        public int? TotalHoursLogged { get; set; }

        public int? TotalEmployees { get; set; }

        public double? AverageHoursPerEmployee { get; set; }
    }
}