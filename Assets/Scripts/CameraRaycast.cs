using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour {

	public Layer[] layerPriority = {
		Layer.Enemy, 
		Layer.Walkable
	};

	float rayDistance = 100f;
	Camera viewCamera;

	RaycastHit m_hit;
	public RaycastHit hit {				// public the m_hit to other scripts to use but is READ-ONLY because it doesn't have a setter
		get { return m_hit; }
	}

	Layer m_layerHit;
	public Layer currentLayerHit {				// public the m_layerHit to other scripts to use but is READ-ONLY because it doesn't have a setter
		get { return m_layerHit; }
	}
	Layer lastLayerHit;

	// Delegate and Event
	public delegate void LayerChangeDelegate (Layer currentLayer);
	public event LayerChangeDelegate LayerChangeEvent;

	void Awake () {
		viewCamera = Camera.main;
	}

	void FixedUpdate () {
		if (m_layerHit != lastLayerHit) {
			if (LayerChangeEvent != null) {
				LayerChangeEvent (m_layerHit);
			}
			print ("Layer Changed");
		}

		lastLayerHit = m_layerHit;

		foreach (Layer layer in layerPriority) {
			var hitInfo = RaycastToLayer (layer);
			if (hitInfo.HasValue) {
				m_hit = hitInfo.Value;
				m_layerHit = layer;
		
				return;							// Exit out of Update method if Raycast has hit one layer
			}
		}

		// If Raycast didn't hit any of the layers. Raycast will hit background.
		m_hit.distance = rayDistance;
		m_layerHit = Layer.RaycastEndStop;

	}

	RaycastHit? RaycastToLayer (Layer layer) {

		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit rayHit;

		// If Raycast hit the layer being tested against. It will return the Hit info.
		if (Physics.Raycast (ray, out rayHit, rayDistance, 1 << (int)layer)) {
			return rayHit;
		}

		return null;
	}
}
