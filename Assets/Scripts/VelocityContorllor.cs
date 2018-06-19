using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityContorllor : MonoBehaviour {

	PlayerContorller contorller;
	Rigidbody rb;
	public GameObject velocityCenter;
	public float rotationSpeed;
	public float rotSpeedIncr;
	public float maxRotSpeed;
	public float velocityMultiplyer;
	public bool startRotating;
	public bool animStallDone;
	public bool canFling;


	void Start () {
		contorller = GetComponent<PlayerContorller> ();
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {

		float x = contorller.x;

		if (!contorller.isHolding) {

			startRotating = false;
			animStallDone = false;
		}
		if (contorller.isHolding && !animStallDone) {
			Invoke ("AnimationStall", 0.1f);
			animStallDone = true;
			rotationSpeed = 0;
		}

		if (contorller.isHolding && animStallDone) {
			canFling = true;
		}

		if (canFling && !contorller.isHolding) {
			rb.velocity = velocityCenter.transform.right * rotationSpeed * velocityMultiplyer;
			Invoke("LetGo", 0.0f);
		
		}

		if (startRotating) {
			if(contorller.isFacingRight)
				transform.Rotate (0, 0, rotationSpeed);
			if(!contorller.isFacingRight)
				transform.Rotate (0, 0, -rotationSpeed);
		}
	}

	void LetGo(){
		canFling = false;
		rotationSpeed = 0;
		transform.rotation = Quaternion.identity;
	}

	void AnimationStall(){
		startRotating = true;
		StartCoroutine (IncreaseRotationSpeed ());
	}

	IEnumerator IncreaseRotationSpeed(){
		
	yield return new WaitForSeconds (0.1f);

		rotationSpeed += rotSpeedIncr;

		if (rotationSpeed < maxRotSpeed && Input.GetButton ("Fire2"))
		StartCoroutine (IncreaseRotationSpeed ());
		

	}
}
