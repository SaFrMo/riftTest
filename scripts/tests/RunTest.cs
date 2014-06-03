using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunTest : MonoBehaviour {

	private Timer t = null;
	public string instructions = "In this block, your display will...";
	public string debriefing = "Thank you for your participation!";
	public List<GameObject> allObjects = new List<GameObject>();
	private bool testing = false;
	public Test CurrentTest = null;
	private float startTime = 0;
	private float currentTrial = 0;
	private float maxTrials = 7;

	public void NextTrial (bool correctObject) 
	{
		if (correctObject) { CurrentTest.Correct(); }
		CurrentTest.Complete();
		// start a new trial
		if (currentTrial < maxTrials)
			StartTrial();
		// reset and start a new block
		else
		{
			currentTrial = 0;
			_position = Position.Instructions;
		}
	}

	// The main trial function.
	private void StartTrial ()
	{
		// finish old test
		if (CurrentTest != null) { CurrentTest.Complete(); }

		// count next one
		currentTrial++;

		// clear old objects if applicable
		if (allObjects.Count > 0)
		{
			GameObject[] allArray = allObjects.ToArray();
			foreach (GameObject go in allArray)
			{
				Destroy (go);
			}
			allObjects.Clear();
		}

		// flag that we're testing
		testing = true;
		
		// BLOCK GENERATION 
		// ======================
		ObjectSpawner o = GetComponent<ObjectSpawner>();
		// generate correct answer
		o.PrepareTest();
		// calculate how many moderate matches to generate - subtract one to account for the perfect match
		int theRest = AdjustParameters.ObjectsToAppear - 1;
		int moderateMatches = Mathf.RoundToInt (theRest * (AdjustParameters.ModerateMatches / 100f));
		int badMatches = theRest - moderateMatches;
		// generate perfect match
		GameObject perfect = o.GenerateObject (ObjectSpawner.ObjectType.Perfect);
		allObjects.Add (perfect);
		// generate moderate matches
		for (int i = 0; i < moderateMatches; i++) { allObjects.Add (o.GenerateObject(ObjectSpawner.ObjectType.Moderate)); }
		// generate bad matches
		for (int i = 0; i < badMatches; i++) { allObjects.Add (o.GenerateObject(ObjectSpawner.ObjectType.Bad)); }

		// INFORMATION DISPLAY
		// ======================
		string glassDisplayType = string.Empty;
		string glassDisplayDuration = string.Empty;
		switch (_position)
		{
		case Position.BlockOne:
			// nothing special necessary here
			break;
		};

		// save this data to a new Test
		CurrentTest = new Test (_position,
		                        o.GetCorrectAnswer(),
		                        glassDisplayType,
		                        glassDisplayDuration);

		// TODO: include this code somewhere to save test results
		UserDataMaster.CURRENT_USER.tests.Add (CurrentTest);
	}

	private List<Position> allTests = new List<Position>()
	{
		Position.BlockOne, // no information (control)
		Position.BlockTwo,
		Position.BlockThree,
		Position.BlockFour,
		Position.BlockFive,
		Position.BlockSix,
		Position.BlockSeven
	};

	public enum Position
	{
		Instructions,
		BlockOne,
		BlockTwo,
		BlockThree,
		BlockFour,
		BlockFive,
		BlockSix,
		BlockSeven,
		Debriefing
	}

	// determines what's happening now
	private Position _position = Position.Instructions;

	// instructions GUI
	private string glassInstructions = "test";
	private void OnGUI ()
	{
		if (_position != Position.Instructions)
		{
			// create the Google Glass window as a 16:10 window
			float width = Screen.width / 4f;
			float height = width / 1.6f;
			float spacer = 10f;
			GUI.Box (new Rect (Screen.width - width - spacer,
			                               spacer,
			                               width,
			                               height), glassInstructions);
		}




		switch (_position)
		{

			// display instructions
		case Position.Instructions:
			if (allTests.Count > 0)
			{
				// clear all objects
				if (allObjects.Count > 0)
				{
					GameObject[] allArray = allObjects.ToArray();
					foreach (GameObject go in allArray)
					{
						Destroy (go);
					}
					allObjects.Clear();
					t = null;
				}
				// instructions window
				float width = Screen.width / 2;
				float height = Screen.height / 2;
				GUILayout.BeginArea (new Rect (width - width / 2,
				                               height - height / 2,
				                               width,
				                               height));
				GUILayout.Box (instructions + "\nTrial will begin in 3 seconds.");

				// Countdown timer
				if (t == null)
				{
					t = new Timer (3f);
				}
				if (t.RunTimer())
				{
					if (allTests.Count > 0)
					{
						// loads the next test and deletes the reference to that block to avoid repeats
						int which = UnityEngine.Random.Range (0, allTests.Count);
						_position = allTests[which];
						allTests.RemoveAt (which);
						StartTrial();
					}

				}
				GUILayout.EndArea();

			}
			else
			{
				UserDataMaster.SaveData();
				_position = Position.Debriefing;
			}
			break;

			// debriefing window
		case Position.Debriefing:
			float w = Screen.width / 2;
			float h = Screen.height / 2;
			GUILayout.BeginArea (new Rect (w - w / 2,
			                               h - h / 2,
			                               w,
			                               h));
			GUILayout.Box (debriefing);
			if (GUILayout.Button ("Done")) { Application.LoadLevel ("welcome"); }
			GUILayout.EndArea();
			break;
		};

	}
}
