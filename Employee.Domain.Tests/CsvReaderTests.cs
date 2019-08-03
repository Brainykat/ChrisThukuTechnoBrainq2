using System.Threading.Tasks;
using Xunit;

namespace Employee.Domain.Tests
{
	public class CsvReaderTests
	{
		[Fact]
		public async Task GetEmployees_ReturnsListOfAmployees_WhenCalled()
		{
			CsvReader csvReader = new CsvReader();
			var result = await csvReader.GetEmployees("data.csv");
			Assert.Equal(6, result.Count);
		}
	}
}
