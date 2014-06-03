using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WelcomeScreen : MonoBehaviour {

	// GUI skin
	public GUISkin skin;

	private bool SubmitInformation()
	{
		// make sure the user number and age can be parsed into integers
		int test;
		if (!int.TryParse (_userNumber, out test)) { _userNumber = string.Empty; return false; }
		if (!int.TryParse (_userAge, out test)) { _userAge = string.Empty; return false; }

		// if they're good, create the relevant User Data and a reference to it
		UserData ud = new UserData(_userNumber, _userAge, _userGender);
		UserDataMaster.USER_DICT.Add (ud.UserNumber, ud);	
		UserDataMaster.CURRENT_USER = ud;
		//UserDataMaster.SaveData();
		Application.LoadLevel("test1");
		return true;
	}

	// user variables
	private string _userNumber = string.Empty;
	private string _userAge = string.Empty;
	private string _userGender = string.Empty;

	// error entering variables
	private bool error = false;

	private void OnGUI ()
	{
		if (!AdjustParameters.showWindow)
		{
			GUI.skin = skin;

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
				if (!SubmitInformation())
				{
					error = true;
				}


			}

			// error box
			if (error)
			{
				GUILayout.Box ("Could not submit data! Please make sure the \"User Number\" and \"Age\" fields are numbers only.");
			}

			GUILayout.EndArea();
		}


	}
}
