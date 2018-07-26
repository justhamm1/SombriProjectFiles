using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speedMultipler;
	float speedStartValue;
	public float jumpForce;
	public float jumpTime;
	public float diForce;
	float x;
	float y;
	float t = 0;
	public float groundedSpeed;

	Rigidbody rb;
	public bool holdingJump;
	public bool grounded;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		speedStartValue = speedMultipler;
	}
	

	void Update () {

		//Inputs
		x = Input.GetAxisRaw ("Horizontal");
		y = Input.GetAxisRaw ("Vertical");

		groundedSpeed = x * speedMultipler * Time.deltaTime;

		//Transform Player
		transform.Translate (groundedSpeed, 0, 0);
		if (x > 0) {transform.GetChild (0).localRotation = Quaternion.identity;}
		if (x < 0) {transform.GetChild (0).localRotation = Quaternion.Euler (0, 180, 0);}

		//Jump Button
		if (Input.GetButtonDown ("Fire1") && grounded) {
			holdingJump = true;
			rb.velocity = new Vector3 (0, 0, 0);
			Invoke ("StopJump", jumpTime);
		}
		if (Input.GetButtonUp ("Fire1")) {
			holdingJump = false;
			CancelInvoke ("StopJump");
		}

		if (!grounded) {
			t += 0.9f * Time.deltaTime;
			speedMultipler = Mathf.Lerp (speedStartValue, 0, t);
		}
	}
	void FixedUpdate(){

		//Add Force while holding Jump
		if (holdingJump) {
			rb.AddForce(0, jumpForce ,0);
		}
		x = Input.GetAxis ("Horizontal") * speedStartValue * Time.deltaTime;
		if (!grounded) {
			rb.AddForce (x * diForce, 0, 0);
		}
	}

	void StopJump(){
		holdingJump = false;
	}

	void OnCollisionEnter(Collision other){
		
		if (other.gameObject.tag == "Untagged") {
			grounded = true;
			t = 0;
			speedMultipler = speedStartValue;
			rb.velocity = rb.velocity / 2;
		}

	}
	void OnCollisionExit(Collision other){
		
		if (other.gameObject.tag == "Untagged") {
			grounded = false;
		}
	}
}
