using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeBehavior : MonoBehaviour {

	GameObject player;
	Transform getUpSpot;
	Rigidbody playerRb;
	PlayerContorller controller;
	Animator playerAnim;

	bool entered;
	public bool leftFacingLedge;

	void Start () {
		player = GameObject.Find ("Player Holder"); 
		playerRb = player.GetComponent<Rigidbody> ();
		controller = player.GetComponent<PlayerContorller> ();
		playerAnim = player.GetComponent<AnimationController> ().anim;
		getUpSpot = transform.GetChild (0).transform;
	}
	

	void OnTriggerEnter(Collider other) {

		if (other.tag == "LedgeChecker") {

			entered = true;
			CancelInvoke ();
			Invoke ("Stop", 0.2f);
			playerAnim.SetBool ("On Ledge", entered);
		}

	}

	void Update(){
		playerAnim.SetBool ("On Ledge", entered);

		if (entered) {
			if (!leftFacingLedge && !controller.isFacingRight) {
				controller.gameObject.transform.GetChild (0).localRotation = Quaternion.Euler (0, 180, 0);
				player.transform.position = Vector3.Lerp(player.transform.position, getUpSpot.position, Time.deltaTime*10);
				playerRb.isKinematic = true;
				controller.enabled = false;
			}
			if (leftFacingLedge  && controller.isFacingRight) {
				controller.gameObject.transform.GetChild (0).localRotation = Quaternion.identity;
				player.transform.position = Vector3.Lerp(player.transform.position, getUpSpot.position, Time.deltaTime*10);
				playerRb.isKinematic = true;
				controller.enabled = false;
			}
				
		}

	}
	void Stop(){
		entered = false;
		playerRb.isKinematic = false;
		controller.enabled = true;
	}
}
