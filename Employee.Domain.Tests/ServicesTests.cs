using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Employee.Domain.Tests
{
	public class ServicesTests
	{
		[Fact]
		public void ValidateEmployees_ThrowsException_WhenTheresMoreThanOneCeo()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1",null,100),
				Employee.Create("Employee2","",100)
			};
			EmployeesServices services = new EmployeesServices(employees);
			services.ValidateEmployees();
			Assert.False(services.IsValid);
			Assert.Contains(services.ValidationErrors, m => m.Message == "More than one CEO listed");
		}

		[Fact]
		public void ValidateEmployees_ThrowsException_WhenSomeManagersAreNotListed()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1","Employee2",100),
				Employee.Create("Employee2","Employee3",100)
			};
			EmployeesServices services = new EmployeesServices(employees);
			services.ValidateEmployees();
			Assert.False(services.IsValid);
			Assert.Contains(services.ValidationErrors, m => m.Message == "Some Managers not listed");
		}

		[Fact]
		public void ValidateEmployees_ThrowsException_WhenEmployeeHasMoreThanOneManager()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1","",100),
				Employee.Create("Employee2","Employee1",100),
				Employee.Create("Employee3","Employee1",100),
				Employee.Create("Employee3","Employee2",100)
			};
			EmployeesServices services = new EmployeesServices(employees);
			services.ValidateEmployees();
			Assert.False(services.IsValid);
			Assert.Contains(services.ValidationErrors, m => m.Message == "Employee Employee3 has more than one manager");
		}

		[Fact]
		public void ValidateEmployees_ThrowsException_WhenEmployeesHaveCyclicReference()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee0","",100),
				Employee.Create("Employee2","Employee1",100),
				Employee.Create("Employee1","Employee2",100)
			};
			EmployeesServices services = new EmployeesServices(employees);
			services.ValidateEmployees();
			Assert.False(services.IsValid);
			Assert.Contains(services.ValidationErrors, m => m.Message == "Cyclic Reference detected");
		}
		[Theory]
		[InlineData("Employee2", 1800)]
		[InlineData("Employee3", 500)]
		[InlineData("Employee1", 3800)]
		public void GetManagersBudget_ReturnsBudget(string managerId, long expected)
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee1","",1000),
				Employee.Create("Employee2","Employee1",800),
				Employee.Create("Employee3","Employee1",500),
				Employee.Create("Employee4","Employee2",500),
				Employee.Create("Employee6","Employee2",500),
				Employee.Create("Employee5","Employee1",500)
			};
			EmployeesServices services = new EmployeesServices(employees);
			var result = services.GetManagersBudget(managerId);
			Assert.Equal(expected, result);
		}
	}
}
