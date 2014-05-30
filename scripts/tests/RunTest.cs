using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunTest : MonoBehaviour {

	public string instructions = "Instructions go here.";

	private List<Position> allTests = new List<Position>()
	{
		Position.BlockOne, 
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

	private void OnGUI ()
	{
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
