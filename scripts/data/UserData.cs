﻿using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;

public class UserData {

	public UserData (string _number, string _age, string _gender)
	{
		try
		{ 
			UserNumber = int.Parse(_number);
			Age = int.Parse(_age);
		}
		catch
		{
			MonoBehaviour.print ("Couldn't parse number/age!");
			return;
		}
		Gender = _gender;
	}

	public int UserNumber { get; private set; }
	public int Age { get; private set; }
	public string Gender { get; private set; }

	public List<Test> tests = new List<Test>();
}

/* 
 * Required data from tests
 * table.Columns.Add ("Block", typeof(string));
		table.Columns.Add ("Target Dimensions", typeof(string));
		table.Columns.Add ("Object Clicked", typeof(string)); // correct or incorrect object
		table.Columns.Add ("Glass Display Type", typeof(string));
		table.Columns.Add ("Glass Display Duration", typeof(string));
		table.Columns.Add ("Reaction time", typeof(float));
*/

public class Test
{
	public RunTest.Position Block { get; private set; }
	public string Dimensions { get; private set; }
	public bool ClickedCorrect { get; private set; }
	public string GlassDisplayType { get; private set; }
	public string GlassDisplayDuration { get; private set; }
	private float startTime;
	private float endTime;
	public float ReactionTime { get { return endTime - startTime; } }

	// constructor
	public Test (RunTest.Position block,
	             string dimensions,
	             string glassDisplayType,
	             string glassDisplayDuration)
	{
		Block = block;
		Dimensions = dimensions;
		GlassDisplayType = glassDisplayType;
		GlassDisplayDuration = glassDisplayDuration;
		startTime = Time.time;
	}

	// user clicked the correct object
	public void Correct()
	{
		ClickedCorrect = true;
	}

	public void Complete()
	{
		endTime = Time.time;
	}
}
