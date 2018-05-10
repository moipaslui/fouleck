using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (IsometricObject))]
[CanEditMultipleObjects]
public class IsometricObjectEditor : Editor
{
	public void OnSceneGUI()
	{
		IsometricObject obj = (IsometricObject)target;

		SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
		obj.spriteLowerBound = spriteRenderer.bounds.size.y * 0.5f;
		obj.spriteHalfWidth = spriteRenderer.bounds.size.x * 0.5f;

		if(!Application.isPlaying)
			obj.PlaceInZ();
	}
}
