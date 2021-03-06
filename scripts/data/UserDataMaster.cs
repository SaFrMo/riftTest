﻿using UnityEngine;
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

	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.E)) { SaveData(); }
	}

	// export data to data table
	public static DataTable ExportData ()
	{
		DataTable table = new DataTable();
		
		// basic user data: number, age, and gender
		table.Columns.Add ("User Number", typeof(int));
		table.Columns.Add ("Age", typeof(int));
		table.Columns.Add ("Gender", typeof(string));

		// test data
		table.Columns.Add ("Block", typeof(string));
		table.Columns.Add ("Target Dimensions", typeof(string));
		table.Columns.Add ("Object Clicked", typeof(string)); // correct or incorrect object
		table.Columns.Add ("Glass Display Type", typeof(string));
		table.Columns.Add ("Glass Display Duration", typeof(string));
		table.Columns.Add ("Reaction time", typeof(float));

		// go through all saved user values and export them as an Excel table
		foreach (KeyValuePair<int, UserData> kv in USER_DICT)
		{
			UserData d = kv.Value;
			CURRENT_USER = d;
			// add basic data
			table.Rows.Add (d.UserNumber, d.Age, d.Gender);
			// save test data
			if (d.tests.Count > 0)
			{
				foreach (Test t in d.tests)
				{
					table.Rows.Add (null,
					                null,
					                null,
					                t.Block.ToString(),
					                t.Dimensions,
					                t.ClickedCorrect.ToString(),
					                t.GlassDisplayType,
					                t.GlassDisplayDuration,
					                t.ReactionTime);
				}
			}
		}

		return table;
	}

	// save data as Excel-compatible file
	public static void SaveData ()
	{
		DataTable t = ExportData ();

		string filename = "User" + CURRENT_USER.UserNumber.ToString() + ".csv";

		using (StreamWriter writer = new StreamWriter("C:\\" + filename)) {
			Rfc4180Writer.WriteDataTable(t, writer, true); }

	}

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
