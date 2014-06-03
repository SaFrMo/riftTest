using UnityEngine;
using System.Collections;

public class RotateMe : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up, 1f);
	}
}
