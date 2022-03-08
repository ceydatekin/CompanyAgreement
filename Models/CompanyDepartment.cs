namespace CompanyAgreement.Models
{
    public class CompanyDepartment
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
