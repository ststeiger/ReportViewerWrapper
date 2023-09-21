
namespace Embedded2017.SubReportExample
{

    // https://www.c-sharpcorner.com/article/rdlc-subreport-using-c-sharp-and-wpf/
    public class Employee
    {
        public int BE_ID { get; set; }
        public string BE_Name { get; set; }


        public static System.Collections.Generic.IEnumerable<Employee> GetEmployees()
        {
            System.Collections.Generic.IEnumerable<Employee> departments = new System.Collections.Generic.List<Employee>()
           {
               new Employee() {BE_ID = 1, BE_Name = "Applied Mathematics" },
               new Employee() {BE_ID = 2, BE_Name = "Software" },
               new Employee() {BE_ID = 3, BE_Name = "Machine Learning" },
               new Employee() {BE_ID = 4, BE_Name = "Petroleum Engineering" },
           };
            return departments;
        }


    }

}