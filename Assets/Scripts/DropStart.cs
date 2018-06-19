using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStart : MonoBehaviour {

	public GameObject platform;
	public GameObject rocks;
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			platform.SetActive (false);
			rocks.SetActive (true);
			rb.isKinematic = false;
		}
	}
}
