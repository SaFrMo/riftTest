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
		_correctColor = availableColors[r.Next(0, 2)];
		_correctTexture = availablePatterns[r.Next(0, 2)];
		_correctShape = availableShapes[r.Next(0, 2)];
		print (_correctColor);
		print (_correctShape);
		print (_correctTexture);
	}

	private Color GenerateBadColor ()
	{
		Color _color;
		do { _color = availableColors[r.Next(0, 2)]; } 
		while (_color == _correctColor);
		return _color;
	}

	private Texture2D GenerateBadTexture ()
	{
		Texture2D t;
		do { t = availablePatterns[r.Next(0, 2)]; } 
		while (t == _correctTexture);
		return t;
	}

	private PrimitiveType GenerateBadShape ()
	{
		PrimitiveType p;
		do { p = availableShapes[r.Next(0, 3)]; } 
		while (p == _correctShape);
		return p;
	}
    
    
    public GameObject GenerateObject (ObjectType t = ObjectType.Moderate)
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

		// put at a random position
		do
		{
			float posX = (float)(AdjustParameters.XOnlyOnscreen ? 
			              r.Next (0, 100) / 100f :
			              r.Next (-50, 150) / 100f);
			result.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (posX,
			                                                                           r.Next(0, 100) / 100f,
			                                                                           5f)); 
		} while (Physics.OverlapSphere(result.transform.position, result.renderer.bounds.extents.magnitude).Length != 0);
		// add a script to manage mouse click's behavior
		result.AddComponent<IAmAnObject>();

		// save as the correct answer if applicable
		if (t == ObjectType.Perfect) { CORRECT_ANSWER = result; }
        
        return result;
    }

	private void Start ()
	{
		r = new System.Random();
	}

	// EXPOSED METHODS
	// =================

	public void PrepareTest()
	{
		GenerateAnswer();
	}
}