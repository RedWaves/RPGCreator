using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor;
	[SerializeField] Texture2D targetCursor;
	[SerializeField] Texture2D crossCursor;

	CameraRaycast camRay;
	GameObject player;

	void Start () {
		camRay = GetComponent<CameraRaycast> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		camRay.LayerChangeEvent += OnLayerChange;
	}

	void OnLayerChange (Layer currentLayer) {
		Debug.Log ("Cursor on new layer " + currentLayer);
		switch (currentLayer) {
		case Layer.Enemy:
			Cursor.SetCursor (targetCursor, Vector2.zero, CursorMode.Auto);
			break;
		case Layer.Walkable:
			Cursor.SetCursor (walkCursor, Vector2.zero, CursorMode.Auto);
			break;
		default:
			Cursor.SetCursor (crossCursor, Vector2.zero, CursorMode.Auto);
			break;	
		}
	}

}
