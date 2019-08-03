using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Domain
{
	public interface ICsvReader
	{
		Task<List<Employee>> GetEmployees(string csvFilePath);
	}
}