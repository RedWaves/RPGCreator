using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject target;
	Vector3 offset;

	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
		offset = transform.position - target.transform.position;
	}

	void LateUpdate () {
		if (target == null) {
			return;
		}

		transform.position = target.transform.position + offset;
	}
}
