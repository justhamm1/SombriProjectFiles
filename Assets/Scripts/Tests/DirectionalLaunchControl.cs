using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLaunchControl : MonoBehaviour {

	public GameObject arrow;
	public GameObject arrowHolder;
	public GameObject arrowHolderHolder;
	PlayerContorller contorller;
	Rigidbody rb;
	//public GameObject velocityCenter;
	public float rotationSpeed;
	public float rotSpeedIncr;
	public float maxRotSpeed;
	public float velocityMultiplyer;
	public bool startRotating;
	public bool animStallDone;
	public bool canFling;
	Vector3 arrowDirection;


	void Start () {
		contorller = GetComponent<PlayerContorller> ();
		rb = GetComponent<Rigidbody> ();
		arrowHolderHolder.transform.parent = null;
	}

	void LateUpdate () {
		arrowHolderHolder.transform.rotation = Quaternion.identity;
		arrowHolderHolder.transform.position = transform.position;

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
			float y = Input.GetAxisRaw("Vertical");
			arrow.SetActive (true);
			arrowDirection = Vector3.up * x + Vector3.right * -y;

			if (arrowDirection.sqrMagnitude > 0.0f) {

				arrowHolder.transform.rotation = Quaternion.LookRotation (arrowDirection, Vector3.forward);
			}
	}

		if (canFling && !contorller.isHolding) {
			rb.velocity = -arrowHolder.transform.right * rotationSpeed * velocityMultiplyer;
			Invoke("LetGo", 0.0f);
			arrow.SetActive (false);
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

	void Update(){
		arrowHolderHolder.transform.rotation = Quaternion.identity;
	}
}
