using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

	Camera camera;

	Animator hitAnimation;
	Transform hammerPos;

	void Start() {
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		hammerPos = transform.parent.transform;
		hitAnimation = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) {
			if (hit.transform.tag == "Table") {
				Transform objectHit = hit.transform;
				hammerPos.position = new Vector3 (hit.point.x, hammerPos.position.y, hit.point.z);
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			hitAnimation.SetTrigger("PlayHit");
		}
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log ("Hit");
		if (other.transform.tag == "Sword") {
			Debug.Log ("HitSword");
			other.transform.GetComponent<Sword> ().HammerHit (5);
		}
	}
}
