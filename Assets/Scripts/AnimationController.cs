using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour {

	PlayerContorller contorller;
	public Animator anim;
	public bool isWalking;
	public bool jumpPressed;
	public Collider capsule;
	public GameObject ballForm;
	public GameObject notBallForm;
	public GameObject animatedPlayer;
	public GameObject ragdoll;
	public GameObject inWall;
	public GameObject inHand;
	public GameObject walkingAudio;
	public GameObject pauseMenu;
	bool deadDone;

	void Start () {
		contorller = GetComponent<PlayerContorller> ();

	}
	

	void Update () {
		//Set Animator
		anim.SetBool ("Is Walking", isWalking);
		anim.SetBool ("In Air", jumpPressed);
		anim.SetBool ("Is Falling", contorller.isFalling);
		anim.SetBool ("Is Hanging", contorller.isHolding);

		//Walking
		if (contorller.x != 0 && !contorller.inAir) {
			isWalking = true;
			walkingAudio.SetActive (true);
		} else {
			isWalking = false;
			walkingAudio.SetActive (false);
		}

		if (PlayerContorller.isPaused) {
			pauseMenu.SetActive (true);
		}
		if (!PlayerContorller.isPaused) {
			pauseMenu.SetActive (false);
			Time.timeScale = 1;
			Cursor.visible = false;
		}
		if (Input.GetButtonDown ("Pause")) {
			PlayerContorller.isPaused = !PlayerContorller.isPaused;
		}

		if (!contorller.dead) {
			if (contorller.inAir && !contorller.isFalling) {
				capsule.enabled = false;
				ballForm.SetActive (true);
				notBallForm.SetActive (false);
				jumpPressed = true;
				if (contorller.hasUmbrella) {
					inHand.SetActive (true);
				} else
					inHand.SetActive (false);
			}
			if (!contorller.inAir || contorller.isHolding || contorller.isFalling) {
				if (contorller.hasUmbrella) {
					inHand.SetActive (true);
				} else
					inHand.SetActive (false);
				capsule.enabled = true;
				ballForm.SetActive (false);
				notBallForm.SetActive (true);
				jumpPressed = false;
			}
			if (contorller.isHolding) {
				inWall.SetActive (true);
				inHand.SetActive (false);
			} else
				inWall.SetActive (false);

		}
		if(contorller.dead && !deadDone){
			anim.SetBool ("Is Falling", true);
			ragdoll.SetActive (true);
			ragdoll.transform.parent = null;
			ragdoll.GetComponent<Rigidbody> ().velocity = 1*contorller.gameObject.GetComponent<Rigidbody> ().velocity;
			animatedPlayer.SetActive (false);
			contorller.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			Invoke ("IsDead", 1);
			deadDone = true;
		}

	
	}

	void IsDead(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

}
