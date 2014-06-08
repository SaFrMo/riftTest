using UnityEngine;
using System.Collections;

public class alpha : MonoBehaviour {

	private void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Application.Quit();
		}
	}
}
