using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContorller : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public float jumpTime;
	public float distance;
	public float jumpResetDistance;
	public float latDrag;
	public float diForce;
	public float dragIncr;
	public float maxLatDrag;
	public float x;
	public int maxSwaangs;
	public int swaangsLeft;
	public float holdTime;
	float maxHoldTime;
	public bool isFalling;
	public bool cantStab;
	public GameObject rocks;
	public GameObject sparks;
	public bool dead;
	public PhysicMaterial bouncy;
	public GameObject deathPoof;
	Rigidbody rb;

	public bool isFacingRight;
	bool jumpHeld;
	public bool canJump;
	public bool inAir;
	public bool isHolding;
	bool calledLatDrag;
	bool done;

	AudioSource sound;
	public AudioClip[] jumps;
	public AudioClip[] failedStabs;
	public AudioClip[] stabs;
	public bool hasUmbrella;
	public static bool isPaused;
	public float velocityStore;

	public GameObject umbrella;

	void Start () {
		sound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
	}
	

	void Update () {

		if (isPaused) {
			Time.timeScale = 0;
			Cursor.visible = true;
		}
		if (!isPaused) {
			Time.timeScale = 1;
			Cursor.visible = false;
//Player Movement--------------------------
			x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

			if (!isHolding) {
				if (x > 0) {
					isFacingRight = true;
					transform.GetChild (0).localRotation = Quaternion.identity;
				}
				if (x < 0) {
					isFacingRight = false;
					transform.GetChild (0).localRotation = Quaternion.Euler (0, 180, 0);
				}
			}

			if (!inAir) {
				latDrag = 1;
				swaangsLeft = maxSwaangs;
				calledLatDrag = false;
				if (!isHolding) {
					transform.Translate (x, 0, 0);	
				}
				rb.velocity = new Vector3 (0, rb.velocity.y, 0);
			} 
			if (inAir && !canJump) {
				if (!isHolding) {
					transform.Translate (x / latDrag, 0, 0);
					rb.AddForce (x * diForce, 0, 0);

				}
				if (!calledLatDrag) {
					calledLatDrag = true;
					StartCoroutine (IncreaseLatDrag ());
				}
				transform.GetChild (0).gameObject.GetComponent<Collider> ().material = bouncy;
			}


			if (Input.GetButtonDown ("Fire1")) {
				if (canJump) {
					canJump = false;
					jumpHeld = true;
					Invoke ("StopJump", jumpTime);
					int randy = Random.Range (0, jumps.Length);
					sound.PlayOneShot (jumps [randy]);
				}
			}
			if (Input.GetButtonUp ("Fire1")) {
				jumpHeld = false;
			}
			if (jumpHeld) {
				rb.AddForce (0, jumpForce, 0);
			}

//Check if on ground--------------------------
			RaycastHit hit;
			Ray downRay = new Ray (transform.position, -Vector3.up);
			if (Physics.Raycast (downRay, out hit)) {
				distance = hit.distance;
				if (hit.distance < jumpResetDistance + 1) {
					transform.GetChild (0).gameObject.GetComponent<Collider> ().material = null;	
				}

				if (hit.distance <= jumpResetDistance) {
					canJump = true;
					inAir = false;
					isFalling = false;

				}
				if (hit.distance >= jumpResetDistance) {
					canJump = false;
					inAir = true;
				}
			}

//This one fucks your mom--------------------
			if (inAir) {
				if (hasUmbrella) {
					if (swaangsLeft > 0) {
						if (!cantStab) {
							if (Input.GetButtonDown ("Fire2")) {
								velocityStore = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
								rb.isKinematic = true;
								isHolding = true;
								Invoke ("Drop", holdTime);
								isFalling = false;
								int randy = Random.Range (0, stabs.Length);
								sound.Stop ();
								sound.PlayOneShot (stabs [randy]);
								Instantiate (rocks, transform.position, Quaternion.identity);
							}
							if (!Input.GetButton ("Fire2") && isHolding) {
								rb.isKinematic = false;
								isHolding = false;
								swaangsLeft--;
								CancelInvoke ("Drop");
								Invoke ("NotHolding", 0.01f);
							}
						}
						if (cantStab) {
							if (Input.GetButtonDown ("Fire2")) {
								int randy = Random.Range (0, stabs.Length);
								Instantiate (sparks, transform.position, Quaternion.identity);
							}
						}
					}
				}
			}
		}
			
		umbrella.transform.rotation = Quaternion.Euler (0, 0, x * -80f);
		if (Input.GetButton ("Fire3")) {
			rb.drag = 8;
			umbrella.SetActive (true);
			diForce = 130;
		}
		if (Input.GetButtonUp ("Fire3") || isHolding || !inAir) {
			rb.drag = 0;
			umbrella.SetActive (false);
			diForce = 40;
		}
}
	void NotHolding(){
		rb.isKinematic = false;
		isHolding = false;

	}

	IEnumerator IncreaseLatDrag(){
		yield return new WaitForSeconds (0.1f);

		latDrag += dragIncr; // =manInDrag;

		if (latDrag < maxLatDrag)
			StartCoroutine (IncreaseLatDrag ());
	}

	void StopJump(){
		jumpHeld = false;
	}
		
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Spikes" && !done) {
			dead = true;
			Instantiate (deathPoof, transform.position, Quaternion.identity);
			this.enabled = false;
			done = true;
		}

	}
	void OnTriggerStay(Collider other){

		if (other.tag == "Umbrella") {
			hasUmbrella = true;
			Destroy (other.gameObject);
		}
		if (isHolding && other.tag != "Blocks") {
			isHolding = false;
			rb.isKinematic = true;
			Invoke ("NotKinematic", 0.0001f);
			isFalling = true;
			CancelInvoke ("Drop");
		}
	}

	void Drop(){
		isHolding = false;
		rb.isKinematic = true;
		Invoke ("NotKinematic", 0.0001f);
		isFalling = true;

	}

	void NotKinematic(){
		rb.isKinematic = false;
	}
}
