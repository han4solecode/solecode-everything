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

        /*var empFromIT = Employee.GetEmployees().Select(emp => new
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

        var ITannualSalaryMoreThan800K = Employee.GetEmployees().Where(emp => emp.Department == "IT" && (emp.Salary * 12) > 800000);
        foreach (var item in ITannualSalaryMoreThan800K)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Department: {item.Department} | Salary: {item.Salary}");
        }*/

        var departmentList = new List<Department>
        {
            new Department { Id = 1 , Name = "IT"},
            new Department { Id = 2 , Name = "Sales"},
            new Department { Id = 3 , Name = "Finance"}
        };

        var result = from emp in Employee.GetEmployees()
                     join dept in departmentList on emp.DeptId equals dept.Id
                     select new
                     {
                         EmployeeName = string.Concat(emp.FirstName, " ", emp.LastName),
                         Department = dept.Name
                     };

        foreach (var item in result)
        {
            Console.WriteLine($"Fullname: {item.EmployeeName} | Department: {item.Department}");
        }

        var resAny = (from emp in Employee.GetEmployees()
                      select emp).Any(emp => emp.Salary > 100000);
        Console.WriteLine(resAny);

        var resAll = (from emp in Employee.GetEmployees()
                      select emp).All(emp => emp.Salary > 100000);
        Console.WriteLine(resAll);

        var groupBy = (from emp in Employee.GetEmployees()
                       group emp by emp.DeptId);
        foreach (var item in groupBy)
        {
            Console.WriteLine($"{item.Key} : {item.Count()}");
            foreach (var emp in item)
            {
                Console.WriteLine($"Name: {emp.FirstName}");
            }

        }

        var resGroupBy = Employee.GetEmployees()
            .GroupBy(e => e.DeptId)
            .Select(g => new
            {
                DeptID = g.Key,
                avgSalary = g.Average(e => e.Salary)
            });
        foreach (var item in resGroupBy)
        {
            Console.WriteLine($"DeptID: {item.DeptID} | Average salary: {item.avgSalary}");
        }

        var take = Employee.GetEmployees().Take(2).ToList();
        var skip = Employee.GetEmployees().Skip(2).ToList();

        int recordsPerPage = 2;
        int pageNumber = 1;

        var empPage = Employee.GetEmployees()
            .Skip((pageNumber -1) * recordsPerPage)
            .Take(recordsPerPage).ToList();
        foreach (var item in empPage)
        {
            Console.WriteLine($"ID: {item.ID} | First name: {item.FirstName} | Last name: {item.LastName} | Department: {item.DeptId} | Salary: {item.Salary}");
        }

        var distinctItems = Employee.GetEmployees()
                            .Select(emp => emp.FirstName)
                            .Distinct().ToList();
        Console.WriteLine(string.Join(", ", distinctItems));

        List<int> data1 = [1, 2, 3, 4, 5, 6];
        List<int> data2 = [1, 3, 5, 8, 9, 10];

        var intersectList = data1.Intersect(data2).ToList();
        var exceptList = data1.Except(data2).ToList();
        Console.WriteLine(string.Join(", ", intersectList));
        Console.WriteLine(string.Join(", ", exceptList));

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
