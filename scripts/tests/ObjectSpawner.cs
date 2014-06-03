using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
	// the single object - this is the correct choice
	public static GameObject CORRECT_ANSWER;

	// System.Random to prevent duplicates from Unity.Random
	System.Random r;

    // holds all available colors
    public List<Color> availableColors = new List<Color>();
    // holds all available patterns
    public List<Texture2D> availablePatterns = new List<Texture2D>();
    // holds all available shapes
    public List<PrimitiveType> availableShapes = new List<PrimitiveType>()
    {
        PrimitiveType.Sphere,
        PrimitiveType.Cube,
        PrimitiveType.Capsule
    };

	
	// ANSWER GENERATION
	// ======================
	// Generate 1 correct answer, X% moderate matches, Y% bad matches
	public enum ObjectType
	{
		Perfect,
		Moderate,
		Bad
	}
	
	// these are the correct values - only one will match them all
	Color _correctColor;
	Texture2D _correctTexture;
	PrimitiveType _correctShape;
	
	private void GenerateAnswer ()
	{
		// generate the one correct value
		_correctColor = availableColors[r.Next(0, availableColors.Count)];
		_correctTexture = availablePatterns[r.Next(0, availablePatterns.Count)];
		_correctShape = availableShapes[r.Next(0, availableShapes.Count)];
		print (_correctColor);
		print (_correctShape);
		print (_correctTexture);
	}

	private Color GenerateBadColor ()
	{
		Color _color;
		do { _color = availableColors[r.Next(0, availableColors.Count)]; } 
		while (_color == _correctColor);
		return _color;
	}

	private Texture2D GenerateBadTexture ()
	{
		Texture2D t;
		do { t = availablePatterns[r.Next(0, availablePatterns.Count)]; } 
		while (t == _correctTexture);
		return t;
	}

	private PrimitiveType GenerateBadShape ()
	{
		PrimitiveType p;
		do { p = availableShapes[r.Next(0, availableShapes.Count)]; } 
		while (p == _correctShape);
		return p;
	}
    
    
	public GameObject GenerateObject (ObjectType t = ObjectType.Moderate, bool keepCollider = true)
    {
        // first, generate the object's color
		Color _color = Color.white;
        // generate the object's pattern
		Texture2D _texture = null;
        // and lastly, generate the object's shape
		PrimitiveType _shape = PrimitiveType.Capsule;

		// what kind of match are we making - Perfect, Moderate, or Bad?
		switch (t)
		{
		// generate the one correct answer
		case ObjectType.Perfect:
			_color = _correctColor;
			_texture = _correctTexture;
			_shape = _correctShape;
			break;
			
		// generate a value where one attribute matches
		case ObjectType.Moderate:
			// choose which attribute to match
			int whichIsCorrect = r.Next(0, 2);
			switch (whichIsCorrect)
			{
			case 0:
				_color = _correctColor;
				_texture = GenerateBadTexture();
				_shape = GenerateBadShape();
				break;
			case 1:
				_color = GenerateBadColor();
				_texture = _correctTexture;
				_shape = GenerateBadShape();
				break;
			case 2:
				_color = GenerateBadColor();
				_texture = GenerateBadTexture();
				_shape = _correctShape;
				break;
			};
			break;

		// generate a value where no attributes match
		default:
			_color = GenerateBadColor();
			_texture = GenerateBadTexture();
			_shape = GenerateBadShape();
			break;

		};

		
		
		// REGARDLESS OF WHAT TYPE YOU'RE GENERATING
		// ==========================================
      
        // apply shape, texture, and color
        GameObject result = GameObject.CreatePrimitive (_shape);
		Renderer _render = result.GetComponent<Renderer>();
		_render.material.mainTexture = _texture;
        _render.material.color = _color;
		_render.material.shader = Shader.Find ("Transparent/Diffuse");
		_render.material.color = new Color (_render.material.color.r,
		                                    _render.material.color.g,
		                                    _render.material.color.b,
		                                    1);
		// aesthetic fixes
		if (_render.material.mainTexture.name.Contains ("polka") && _shape != PrimitiveType.Cube) { _render.material.mainTextureScale = new Vector2 (2f, 1f); }
		if (_render.material.mainTexture.name.Contains ("cross") && _shape == PrimitiveType.Cube) { _render.material.mainTextureScale = new Vector2 (.5f, 1f); }

		// put at a random position
		do
		{
			float posX = (float)(AdjustParameters.XOnlyOnscreen ? 
			              r.Next (5, 95) / 100f :
			              r.Next (-50, 150) / 100f);
			result.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (posX,
			                                                                           r.Next(5, 95) / 100f,
			                                                                           5f)); 
		} while (Physics.OverlapSphere(result.transform.position, 1f).Length != 0 ||
		         RunTest.GLASS_DISPLAY_AREA.Contains (Camera.main.WorldToScreenPoint(result.transform.position)));
		// add a script to manage mouse click's behavior
		result.AddComponent<IAmAnObject>();
		// remove the collider if relevant
		if (!keepCollider) 
		{ 
			Destroy (result.collider); 
		}



		// save as the correct answer if applicable
		if (t == ObjectType.Perfect) { CORRECT_ANSWER = result; }

		// scale down object
		result.transform.localScale = Vector3.one * 0.4f;
		if (_shape == PrimitiveType.Capsule) 
		{ result.transform.localScale = new Vector3 (result.transform.localScale.x,
                                                      result.transform.localScale.y * .5f, 
                                                      result.transform.localScale.z); }
        
        return result;
    }

	private void Start ()
	{
		r = new System.Random();
	}

	// return the correct value's parameters as a string for data recording
	public string GetCorrectAnswer()
	{
		// color to human-readable form
		string _col;
		Color compareCol = new Color (_correctColor.r,
		                              _correctColor.g,
		                              _correctColor.b,
		                              1);
		if (compareCol == Color.red) { _col = "Red"; }
		else if (compareCol == new Color (1f, 1f, 0f, 1f)) { _col = "Yellow"; }
		else if (compareCol == Color.blue) { _col = "Blue"; }
		else { _col = "!Color error!"; }

		string _tex = _correctTexture.name;
		string _shape = _correctShape.ToString();
		return string.Format ("{0} {1} {2}", _col, _tex, _shape);
	}


	public void PrepareTest()
	{
		GenerateAnswer();
	}
}