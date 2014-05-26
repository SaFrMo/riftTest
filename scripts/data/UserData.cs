using UnityEngine;
using System.Collections;
using System.Data;

public class UserData {

	public UserData (string _number, string _age, string _gender)
	{
		try
		{ 
			UserNumber = int.Parse(_number);
			Age = int.Parse(_age);
		}
		catch
		{
			MonoBehaviour.print ("Couldn't parse number/age!");
		}
		Gender = _gender;
	}

	public int UserNumber { get; private set; }
	public int Age { get; private set; }
	public string Gender { get; private set; }


}
