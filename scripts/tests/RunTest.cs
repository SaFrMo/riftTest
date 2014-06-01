using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunTest : MonoBehaviour {

	public string instructions = "Instructions go here.";
	public List<GameObject> allObjects = new List<GameObject>();

	// The main trial function.
	private void StartTrial ()
	{
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
		
		// generate blocks
		ObjectSpawner o = GetComponent<ObjectSpawner>();
		for (int i = 0; i < AdjustParameters.ObjectsToAppear; i++)
		{
			allObjects.Add (o.GenerateObject());
		}
		
		
		switch (_position)
		{
			
			
		case Position.BlockOne:
			// nothing special necessary here
			break;
		};
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
		BlockSeven
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
			float width = Screen.width / 2;
			float height = Screen.height / 2;
			GUILayout.BeginArea (new Rect (width - width / 2,
			                               height - height / 2,
			                               width,
			                               height));
			GUILayout.Box (instructions);
			if (GUILayout.Button ("Begin"))
			{
				if (allTests.Count > 0)
				{
					// loads the next test and deletes the reference to that block to avoid repeats
					int which = UnityEngine.Random.Range (0, allTests.Count);
					_position = allTests[which];
					allTests.RemoveAt (which);
					StartTrial();
				}
				else
				{
					print ("DONE");
				}
			}
			GUILayout.EndArea();
			break;
		};

	}
}
