namespace HRIS.Application.DTOs.Worksons
{
    public class WorksonProjectReportResponseDto
    {
        public int? TotalHoursLogged { get; set; }

        public int? TotalEmployees { get; set; }

        public int? AverageHoursPerEmployee { get; set; }
    }
}