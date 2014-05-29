using UnityEngine;
using System.Collections;

public class AdjustParameters : MonoBehaviour {

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Home))
		{
			showWindow = !showWindow;
		}
	}

	public static int ObjectsToAppear = 5;

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






			GUILayout.EndArea ();

		}
	}
}
