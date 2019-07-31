using Xunit;

namespace Employee.Domain.Tests
{
	public class CsvReaderTests
	{
		[Fact]
		public void GetEmployees_ReturnsListOfAmployees_WhenCalled()
		{
			CsvReader csvReader = new CsvReader();
			var result = csvReader.GetEmployees("data.csv");
			Assert.Equal(6, result.Count);
		}
	}
}
