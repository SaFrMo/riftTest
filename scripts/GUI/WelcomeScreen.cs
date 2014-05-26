using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WelcomeScreen : MonoBehaviour {

	private void SubmitInformation()
	{
		UserData ud = new UserData(_userNumber, _userAge, _userGender);
		UserDataMaster.USER_DICT.Add (ud.UserNumber, ud);
		UserDataMaster.SaveData();
	}

	// user variables
	private string _userNumber = string.Empty;
	private string _userAge = string.Empty;
	private string _userGender = string.Empty;

	private void OnGUI ()
	{
		// box dimensions
		float width = Screen.width * .5f;
		float height = Screen.height * .5f;

		GUILayout.BeginArea (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height));

		// WELCOME & INSTRUCTIONS
		// =======================

		// welcome box
		GUILayout.Box ("Welcome!");

		// instructions
		GUILayout.Box ("Instructions go here.");

		// USER INPUT FIELDS
		// ======================

		// number
		GUILayout.BeginHorizontal();
		GUILayout.Box ("Participant number:");
		_userNumber = GUILayout.TextField (_userNumber);
		GUILayout.EndHorizontal();

		// age
		GUILayout.BeginHorizontal();
		GUILayout.Box ("Your age:");
		_userAge = GUILayout.TextField (_userAge);
		GUILayout.EndHorizontal();

		// gender
		GUILayout.BeginHorizontal();
		GUILayout.Box ("Your gender:");
		_userGender = GUILayout.TextField (_userGender);
		GUILayout.EndHorizontal();

		// SUBMIT
		// =====================
		if (GUILayout.Button ("Submit"))
		{
			SubmitInformation();
		}

		GUILayout.EndArea();


	}
}
