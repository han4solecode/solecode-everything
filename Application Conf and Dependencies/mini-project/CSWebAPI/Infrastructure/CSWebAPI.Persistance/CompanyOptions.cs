namespace CSWebAPI.Persistance
{
    public class CompanyOptions
    {
        public const string SettingName = "CompanySettings";

        public int MaxWorkingHours { get; set; }
        public int MaxDeptProject { get; set; }
        public int MaxEmpHandleProject { get; set; }
        public int RetirementAge { get; set; }
        public int ITDeptMaxEmp { get; set; }
    }
}