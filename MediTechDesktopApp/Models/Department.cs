namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a single row in the Departments table.
    /// </summary>
    public class Department
    {
        public int DepartmentId { get; set; }      // matches database column department_id
        public string Name { get; set; }           // matches database column name
    }
}
