using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float stopRadius = 0.2f;

	ThirdPersonCharacter m_Character;
	CameraRaycast camRay;
	Vector3 currentClickTarget;
	bool useGamepadControl = false;

	Transform m_Cam;
	Vector3 m_CamForward;
	Vector3 m_Move;

	void Start () {
		m_Character = GetComponent<ThirdPersonCharacter> ();
		camRay = Camera.main.GetComponent<CameraRaycast> ();
		currentClickTarget = transform.position;

		m_Cam = Camera.main.transform;
	}

	void Update () {
		if (Input.GetButtonDown ("Switch Control")) {
			useGamepadControl = !useGamepadControl;
			currentClickTarget = transform.position;
		}
	}

	void FixedUpdate () {
		if (useGamepadControl) {
			ControlGamepadMovement ();
		} else {
			ControlMouseMovement ();
		}
	}

	void ControlMouseMovement () {
		if (Input.GetMouseButton (1)) {
			switch (camRay.currentLayerHit) {
			case Layer.Walkable:
				currentClickTarget = camRay.hit.point;
				break;
			}
		} 
		Vector3 playerToClick = currentClickTarget - transform.position;
		if (playerToClick.magnitude >= stopRadius) {
			m_Character.Move (playerToClick, false, false);
		} else {
			m_Character.Move (Vector3.zero, false, false);
		}
	}

	void ControlGamepadMovement () {
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

		m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
		m_Move = v*m_CamForward + h*m_Cam.right;

		m_Character.Move(m_Move, false, false);
	}

	void OnGizmosDraw () {
		if (!useGamepadControl) {
			Gizmos.DrawLine (transform.position, currentClickTarget);
		}
	}
}
