using UnityEngine;
using System.Collections;

public class IAmAnObject : MonoBehaviour {

	private void OnMouseDown ()
	{
		print ("CLICKED!");
		if (ObjectSpawner.CORRECT_ANSWER == gameObject)
		{
			print ("CORRECT!");
		}
	}
}
