using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
	// the single object - this is the correct choice

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
    
    
    public GameObject GenerateObject ()
    {
        // first, generate the object's color
        Color _color = availableColors[r.Next(0, 2)];
        // generate the object's pattern
        Texture2D _texture = availablePatterns[r.Next(0, 2)];
        // and lastly, generate the object's shape
        PrimitiveType _shape = availableShapes[r.Next(0, 3)];
        
        // apply shape, texture, and color
        GameObject result = GameObject.CreatePrimitive (_shape);
		Renderer _render = result.GetComponent<Renderer>();
		_render.material.mainTexture = _texture;
        _render.material.color = _color;

		// put at a random position
		float posX = (float)(AdjustParameters.XOnlyOnscreen ? 
		              r.Next (0, 100) / 100f :
		              r.Next (-50, 150) / 100f);
		result.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (posX,
		                                                                           r.Next(0, 100) / 100f,
		                                                                           5f));
		// add a collider for mouseover
		result.AddComponent<BoxCollider>();
		// and a script to manage mouse click's behavior
		result.AddComponent<IAmAnObject>();
        
        return result;
    }

	private void Start ()
	{
		r = new System.Random();
	}
}