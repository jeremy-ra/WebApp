using Sprout.Exam.Entity.Entities;
using System;
using System.Collections.Generic;

namespace Sprout.Exam.UnitTests
{
    public class EmployeeMockData
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>{
                new Employee() { Id = 1, FullName = "John Wick", Birthdate = new DateTime(1986, 10, 1), Tin = "54353454354", TypeId = 1 },
                new Employee() { Id = 2, FullName = "John Does", Birthdate = new DateTime(1985, 04, 23), Tin = "43242324342", TypeId = 2 },
                new Employee() { Id = 3, FullName = "Richelle Dones", Birthdate = new DateTime(1995, 11, 03), Tin = "12345632423", TypeId = 1 }
            };
        }

        public static Employee GetEmployeeById()
        {
            return new Employee() { Id = 3, FullName = "Richelle Dones", Birthdate = new DateTime(1995, 11, 03), Tin = "12345632423", TypeId = 1 };
        }      

        public static List<Employee> GetEmployeesEmpty()
        {
            return new List<Employee>();
        }

        public static Employee NewEmployee()
        {
            return new Employee
            {
                Id = 1,
                FullName = "John Wick",
                Birthdate = new DateTime(1986, 10, 1),
                Tin = "54353454354",
                TypeId = 1
            };
        }
    }
}
