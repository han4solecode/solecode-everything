namespace QueryLinq;

class Program
{
    static void Main(string[] args)
    {
        // \\\\/\/\//\\\\\///////\\\\\\\/\/\/\\\\\\\/////\\/\/\\//\/\\\////
         
        // Linq
        int[] values = { 5, 2, 3, 14, 1, 6, 16, 9, 8, 10, 13, 12, 12, 4, 15, 7 };

        var filtered = from value in values
                       where value > 4
                       select value;

        var sorted = from value in filtered
                     orderby value
                     select value;

        var sortedDescending = from value in filtered
                              orderby value descending
                              select value;

        var filterAndSort = from value in values
                            where value > 8
                            orderby value descending
                            select value;

        var filterMethodSyntax = values.Where(x => x > 4).ToList();

        var sortMethodSyntax = filterMethodSyntax.OrderBy(x => x);

        var mixedSyntaxSum = (from value in values
                             where value > 7
                             select value).Sum();

        Display(sorted, "Value greater than 4 order by default: ");
        Display(sortedDescending, "Value greater than 4 order by descending: ");
        Display(filterAndSort, "Filtered and sorted: ");
        Display(filterMethodSyntax, "Filtered with method syntax: ");
        Display(sortMethodSyntax, "Sorted with method syntax: ");
        // Display(mixedSyntaxSum, "Sum with mixed syntax: ");

        Console.Write("Value greater than 7 sum with mixed syntax: ");
        Console.WriteLine($"{mixedSyntaxSum}");

        // Lambda Expression
        // expression lambda
        int[] num = { 1, 2, 3, 4, 5 };
        var squaredNum = num.Select(x => x * x);
        Console.WriteLine(string.Join(" ", squaredNum));

        // statement lambda


        // Projection select
        var basicQuery = (from emp in Employee.GetEmployees() select emp).ToList();
        foreach (var item in basicQuery)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Salary: {item.Salary}");
        }

        var basicMethod = Employee.GetEmployees().ToList();
        foreach (var item in basicMethod)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Salary: {item.Salary}");
        }

        var selectQuery = (from emp in Employee.GetEmployees()
                           select new Employee()
                           {
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               Salary = emp.Salary,
                           }).ToList();
        foreach (var item in selectQuery)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Salary: {item.Salary}");
        }

        var selectMethod = Employee.GetEmployees().Select(emp => new Employee()
        {
            FirstName= emp.FirstName,
            LastName= emp.LastName,
            Salary = emp.Salary
        }).ToList();
        foreach (var item in selectMethod)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Salary: {item.Salary}");
        }

        var onlyIdQuery = (from emp in Employee.GetEmployees() select emp.ID).ToList();
        foreach (var item in onlyIdQuery)
        {
            Console.Write($" {item}");
        }

        var onlyIdMethod = Employee.GetEmployees().Select(emp => emp.ID).ToList();
        foreach (var item in onlyIdMethod)
        {
            Console.Write($" {item}");
        }

        var anonTypeMethod = Employee.GetEmployees().Select(emp => new
        {
            FullName = string.Concat(emp.FirstName, " ", emp.LastName),
            Salary = emp.Salary
        });
        foreach (var item in anonTypeMethod)
        {
            Console.WriteLine($"Full name: {item.FullName} | Salary: {item.Salary}");
        }

        var anonTypeMethodAnnualSalary = Employee.GetEmployees().Select(emp => new
        {
            FullName = string.Concat(emp.FirstName, " ", emp.LastName),
            AnnualSalary = emp.Salary * 12
        });
        foreach (var item in anonTypeMethodAnnualSalary)
        {
            Console.WriteLine($"Full Name: {item.FullName} | Annual Salary: {item.AnnualSalary}");
        }

        var highestSalary = Employee.GetEmployees().Select(emp => new
        {
            FullName = string.Concat(emp.FirstName, " ", emp.LastName),
            AnnualSalary = emp.Salary * 12
        }).Max(x => x.AnnualSalary);
        Console.WriteLine(highestSalary);

        // var a = anonTypeMethodAnnualSalary.Max(x => x.AnnualSalary);

        var empFromIT = Employee.GetEmployees().Select(emp => new
        {
            FullName = string.Concat(emp.FirstName, " ", emp.LastName),
            Department = emp.Department,
            // AnnualSalary = emp.Salary * 12
        }).Where(emp => emp.Department == "IT");
        foreach (var item in empFromIT)
        {
            Console.WriteLine($"Fullname: {item.FullName} | Department: {item.Department}");
        }

        var annualSalaryMoreThanSixtyK = Employee.GetEmployees().Select(emp => new
        {
            FullName = string.Concat(emp.FirstName, " ", emp.LastName),
            Department = emp.Department,
            AnnualSalary = emp.Salary * 12
        }).Where(emp => emp.AnnualSalary > 60000);
        foreach (var item in annualSalaryMoreThanSixtyK)
        {
            Console.WriteLine($"Fullname: {item.FullName} | Department: {item.Department} | Annual Salary: {item.AnnualSalary}");
        }

        var ITannualSalaryMoreThanSixtyK = Employee.GetEmployees().Where(emp => emp.Department == "IT" && (emp.Salary * 12) > 60000);
        foreach (var item in ITannualSalaryMoreThanSixtyK)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Department: {item.Department} | Salary: {item.Salary}");
        }
    }

    public static void Display(IEnumerable<int> res, string message)
    {
        Console.Write(message);
        foreach (var item in res)
        {
            Console.Write($" {item}");
        }
        Console.WriteLine();
    }
}
