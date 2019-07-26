using Xunit;

namespace Employee.Domain.Tests
{
	public class ReadFromCSVLineTests
	{
		[Fact]
		public void ReadEmployeeFromCSVLine_Returns_NewEmployee()
		{
			string line = "Employee1,Employee0,100";
			var employee = ReadFromCSVLine.ReadEmployeeFromCSVLine(line);
			Assert.Equal("Employee0", employee.ManagerId);
		}
		[Fact]
		public void ReadEmployeeFromCSVLine_ReturnsNull_WhenInputIsInvalid()
		{
			string line = "Employee1,Employee0100";
			var employee = ReadFromCSVLine.ReadEmployeeFromCSVLine(line);
			Assert.Null(employee);
		}
	}
}
