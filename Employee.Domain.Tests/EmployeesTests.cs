

using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Employee.Domain.Tests
{
	public class EmployeesTests
	{
		private readonly Employees _employees;
		private string _cSVPath = @"data.csv";
		private readonly Mock<ICsvReader> _csvReaderMock = new Mock<ICsvReader>();
		public EmployeesTests()
		{
			_employees = new Employees(_cSVPath, _csvReaderMock.Object);
		}
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Employees_ThrowsArgumentNullException_WhenCSVPathIsInvalid(string path)
		{
			Assert.Throws<ArgumentNullException>(() => new Employees(path, _csvReaderMock.Object));
		}
		[Fact]
		public void GetEmployeeRecords_ReturnsNull_WhenListIsNull()
		{
			_csvReaderMock.Setup(k => k.GetEmployees(_cSVPath)).Returns((List<Employee>)null);
			var result = _employees.GetEmployeeRecords();
			Assert.Null(result);
		}
		[Fact]
		public void GetEmployeeRecords_ReturnsEmployees()
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
			_csvReaderMock.Setup(k => k.GetEmployees(_cSVPath)).Returns(employees);
			var result = _employees.GetEmployeeRecords();
			Assert.Equal(6, result.Count);
		}
		[Fact]
		public void GetManagerBudget_ReturnsZero_WhenEmployeeListIsNull()
		{
			_csvReaderMock.Setup(k => k.GetEmployees(_cSVPath)).Returns((List<Employee>)null);
			var result = _employees.GetManagerBudget("Employee1");
			Assert.Equal(0,result);
		}
		[Fact]
		public void GetManagerBudget_ReturnsManagersBudget()
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
			_csvReaderMock.Setup(k => k.GetEmployees(_cSVPath)).Returns(employees);
			var result = _employees.GetManagerBudget("Employee1");
			Assert.Equal(3800, result);
		}
	}
}
