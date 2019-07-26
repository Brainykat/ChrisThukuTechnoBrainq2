namespace Employee.Domain
{
	public static class ReadFromCSVLine
	{
		static public Employee ReadEmployeeFromCSVLine(this string csvLine)
		{
			string[] parts = csvLine.Split(',');
			if (parts.Length == 3)
			{
				string Id = parts[0];
				string ManagerId = parts[1];
				string Salary = parts[2];
				decimal.TryParse(Salary, out decimal salary);
				return Employee.Create(Id, ManagerId, salary);
			}
			return null;
		}
	}
}
