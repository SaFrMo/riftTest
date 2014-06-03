using UnityEngine;
using System.Collections;

public class IAmAnObject : MonoBehaviour {

	private void OnMouseDown ()
	{
		bool correctObject = false;
		if (ObjectSpawner.CORRECT_ANSWER == gameObject)
		{
			correctObject = true;
		}
		GameObject.Find ("Test Manager").GetComponent<RunTest>().NextTrial(correctObject);
	}
}
