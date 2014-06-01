using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
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
        Color _color = availableColors[UnityEngine.Random.Range(0, 2)];
        // generate the object's pattern
        Texture2D _texture = availablePatterns[UnityEngine.Random.Range(0, 2)];
        // and lastly, generate the object's shape
        PrimitiveType _shape = availableShapes[UnityEngine.Random.Range(0, 2)];
        
        // apply shape, texture, and color
        GameObject result = GameObject.CreatePrimitive (_shape);
		Renderer _render = result.GetComponent<Renderer>();
		_render.material.mainTexture = _texture;
        _render.material.color = _color;

		// put at a random position
		float posX = (AdjustParameters.XOnlyOnscreen ? 
		              UnityEngine.Random.Range (0, 100) / 100 :
		              UnityEngine.Random.Range (-50, 150) / 100);
		result.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (posX,
		                                                                           UnityEngine.Random.Range (0, 100) / 100));
        
        return result;
    }
}