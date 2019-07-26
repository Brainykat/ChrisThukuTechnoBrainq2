using System;
using Xunit;

namespace Employee.Domain.Tests
{
	public class EmployeeTests
	{
		[Theory]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData(null)]
		public void Create_ThrowsArgumentNullException_WhenIdIsInvalid(string id)
		{
			Assert.Throws<ArgumentNullException>(nameof(id), () => Employee.Create(id, null, default(decimal)));
		}

		[Fact]
		public void Create_ThrowsArgumentOutOfRangeException_WhenSalaryIsInvalid()
		{
			decimal salary = -1;
			Assert.Throws<ArgumentOutOfRangeException>(nameof(salary), () => Employee.Create("E1", null, salary));
		}
		[Fact]
		public void Create_CreatesNewEmployee()
		{
			var employee = Employee.Create("Employee1", "", 100);
			Assert.Equal("Employee1", employee.Id);
			Assert.Equal(100, employee.Salary);
		}
	}
}
