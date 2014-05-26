using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

public class UserDataMaster : MonoBehaviour {

	public static Dictionary<int, UserData> USER_DICT = new Dictionary<int, UserData>();

	public static UserData CURRENT_USER;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	// export data to .xls file
	public static DataTable ExportData ()
	{
		DataTable table = new DataTable();
		
		// basic user data: number, age, and gender
		table.Columns.Add ("User Number", typeof(int));
		table.Columns.Add ("Age", typeof(int));
		table.Columns.Add ("Gender", typeof(string));

		// go through all saved user values and export them as an Excel table
		foreach (KeyValuePair<int, UserData> kv in USER_DICT)
		{
			UserData d = kv.Value;
			// add basic data
			table.Rows.Add (d.UserNumber, d.Age, d.Gender);

		}

		return table;
	}

	public static void SaveData ()
	{
		DataTable t = ExportData ();

		using (StreamWriter writer = new StreamWriter("C:\\dump.csv")) {
			Rfc4180Writer.WriteDataTable(t, writer, true); }

 	}

	// C# Code

}

public static class Rfc4180Writer 
{
	public static void WriteDataTable(DataTable sourceTable, TextWriter writer, bool includeHeaders) 
	{
		if (includeHeaders) {
			List<string> headerValues = new List<string>();
			foreach (DataColumn column in sourceTable.Columns) {
				headerValues.Add(QuoteValue(column.ColumnName));
			}
			
			writer.WriteLine(string.Join(",", headerValues.ToArray()));
		}
		
		string[] items = null;
		foreach (DataRow row in sourceTable.Rows) {
			items = row.ItemArray.Select(o => QuoteValue(o.ToString())).ToArray();
			writer.WriteLine(string.Join(",", items));
		}
		
		writer.Flush();
	}
	
	private static string QuoteValue(string value) 
	{
		return string.Concat("\"", value.Replace("\"", "\"\""), "\"");
	}
} 
