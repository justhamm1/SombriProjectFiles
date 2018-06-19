using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofOnCollider : MonoBehaviour {

	public GameObject poof;
	public Transform spot;

	void OnCollisionEnter(Collision other){
		
		Instantiate (poof, spot.transform.position, Quaternion.identity);

	}
}
