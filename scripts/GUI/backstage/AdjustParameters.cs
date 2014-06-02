using UnityEngine;
using System.Collections;

public class AdjustParameters : MonoBehaviour {

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			showWindow = !showWindow;
		}
	}

	public static int ObjectsToAppear = 5;
	public static bool XOnlyOnscreen = true;
	public static int ModerateMatches = 50;
	public static int BadMatches;

	public static bool showWindow = false;
	private void OnGUI ()
	{
		if (showWindow)
		{
			float width = Screen.width / 2;
			float height = Screen.height / 2;
			GUILayout.BeginArea (new Rect (width - width / 2,
			                               height - height / 2,
			                               width,
			                               height));

			// adjust number of objects to appear
			GUILayout.Box ("Objects To Appear");
			GUILayout.BeginHorizontal();
			ObjectsToAppear = (int)(GUILayout.HorizontalSlider (ObjectsToAppear, 1, 20));
			GUILayout.Box (ObjectsToAppear.ToString());
			GUILayout.EndHorizontal();

			// adjust where on screen they can appear
			XOnlyOnscreen = GUILayout.Toggle (XOnlyOnscreen, "Only appear on-screen");

			// adjust how many objects are moderate
			BadMatches = 100 - ModerateMatches;
			GUILayout.Box ("Moderate Matches - The rest, " + BadMatches + "%, will be bad matches.");
			GUILayout.BeginHorizontal();
			ModerateMatches = (int)(GUILayout.HorizontalSlider (ModerateMatches, 0, 100));
			GUILayout.Box (ModerateMatches + "%");
			GUILayout.EndHorizontal();






			GUILayout.EndArea ();

		}
	}
}
