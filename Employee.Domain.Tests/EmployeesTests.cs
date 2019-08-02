﻿

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
		public void Employees_ThrowsAggregateException_WhenEmployeesAreInvald()
		{
			List<Employee> employees = new List<Employee>
			{
				Employee.Create("Employee0","",100),
				Employee.Create("Employee2","Employee1",100),
				Employee.Create("Employee1","Employee2",100),
				Employee.Create("Employee1","",1100)
			};
			_csvReaderMock.Setup(k => k.GetEmployees(_cSVPath)).Returns(employees);
			Assert.Throws<AggregateException>(() => _employees.GetEmployeeRecords());
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
		[Theory]
		[InlineData("Employee2",1800)]
		[InlineData("Employee3",500)]
		[InlineData("Employee1", 3800)]
		public void GetManagerBudget_ReturnsManagersBudget(string employeeId, long budget)
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
			var result = _employees.GetManagerBudget(employeeId);
			Assert.Equal(budget, result);
		}
	}
}
