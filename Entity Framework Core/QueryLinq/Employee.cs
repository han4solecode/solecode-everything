﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryLinq
{
    public class Employee
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public int Salary { get; set; }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees =
            [
                new Employee { ID = 101, FirstName = "Preety", LastName = "Tiwary", Salary = 60000, Department = "IT"},
                new Employee { ID = 102, FirstName = "Priyanka", LastName = "Dewangan", Salary = 70000, Department = "IT"},
                new Employee { ID = 103, FirstName = "Hina", LastName = "Sharma", Salary = 80000, Department = "HR"},
                new Employee { ID = 104, FirstName = "Anurag", LastName = "Mohanty", Salary = 90000, Department = "HR"},
                new Employee { ID = 105, FirstName = "Sambit", LastName = "Satapathy", Salary = 100000, Department = "Acc"},
                new Employee { ID = 106, FirstName = "Sushanta", LastName = "Jena", Salary = 160000, Department = "Acc"}
            ];

            return employees;
        }
    }
}