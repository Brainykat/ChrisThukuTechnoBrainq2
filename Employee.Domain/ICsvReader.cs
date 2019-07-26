using System.Collections.Generic;

namespace Employee.Domain
{
	public interface ICsvReader
	{
		List<Employee> GetEmployees(string csvFilePath);
	}
}