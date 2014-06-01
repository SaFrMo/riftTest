﻿using UnityEngine;
using System.Collections;

public class CursorTexture : MonoBehaviour {

	public Texture2D cursorTexture;

	private void Start ()
	{
		Cursor.SetCursor (cursorTexture, new Vector2 (cursorTexture.width / 2, CursorTexture.height / 2);
	}
}
