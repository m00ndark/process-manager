using System.Data;
using System.Linq;
using System.Management;

namespace ProcessManager.DataAccess
{
	public static class WMIHandler
	{
		public static DataTable GetTable(string className, string[] properties)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.AddRange(properties.Select(x => new DataColumn(x)).ToArray());
			ManagementScope scope = new ManagementScope(@"root\CIMV2");
			string columns = string.Join(", ", properties);
			ObjectQuery query = new ObjectQuery("SELECT " + columns + " FROM " + className);
			ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
			foreach (ManagementBaseObject obj in searcher.Get())
			{
				DataRow dataRow = dataTable.NewRow();
				foreach (PropertyData property in obj.Properties)
				{
					try
					{
						dataRow[property.Name] = property.Value;
					}
					catch { }
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
}
