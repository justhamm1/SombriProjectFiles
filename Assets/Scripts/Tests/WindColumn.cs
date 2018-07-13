using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindColumn : MonoBehaviour {

	Rigidbody rb;
	public float windForce;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () {
		
	}


	void OnTriggerStay(Collider other){

		if (other.tag == "Wind") {
			if(Inputs.fire[3])
				rb.AddForce(other.gameObject.transform.up * windForce * 10);
			else 
				rb.AddForce(other.gameObject.transform.up * windForce);
		//	rb.velocity = other.gameObject.transform.up * windForce;
		}
	}

}
