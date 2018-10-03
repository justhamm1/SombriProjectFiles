using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContorller : MonoBehaviour {


	public GameObject GUTS;

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
	bool hitGround;

	AudioSource sound;
	public AudioClip[] jumps;
	public AudioClip[] failedStabs;
	public AudioClip[] stabs;
	public bool hasUmbrella;
	public static bool isPaused;
	public float velocityStore;

	public GameObject umbrella;
	public GameObject wallChecker;

	void Start () {
		sound = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		Application.targetFrameRate = 15;
	}
	

	void FixedUpdate () {
		
		x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;

		if (!inAir) {
			latDrag = 1;
			swaangsLeft = maxSwaangs;
			calledLatDrag = false;
			if (!isHolding) {
				transform.Translate (x, 0, 0);	
			}
		} 

		if (isPaused) {
			Time.timeScale = 0;
			Cursor.visible = true;
		}
		if (!isPaused) {
			Time.timeScale = 1;
			Cursor.visible = false;
//Player Movement--------------------------

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
				
			if (inAir) {
				if (!isHolding) {
					transform.Translate (x / latDrag, 0, 0);
					rb.AddForce (x * diForce, 0, 0);
					wallChecker.SetActive (false);
				}
				if (!calledLatDrag) {
					calledLatDrag = true;
					StartCoroutine (IncreaseLatDrag ());
				}
				transform.GetChild (0).gameObject.GetComponent<Collider> ().material = bouncy;
			}


			if (jumpHeld) {
				rb.velocity = new Vector3 (0, jumpForce, 0);
				jumpForce = jumpForce*1.3f;
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
					hitGround = false;
				}
			}
			
		umbrella.transform.rotation = Quaternion.Euler (0, 0, x * -80f);
		if (Input.GetButton ("Fire3")) {
			rb.drag = 8;
			umbrella.SetActive (true);
			diForce = 220;
		}
		if (Input.GetButtonUp ("Fire3") || isHolding || !inAir) {
			rb.drag = 0;
			umbrella.SetActive (false);
			diForce = 50;
		}

	}//Not Paused --------------
}

	void Update(){



		if (Input.GetButtonDown("Fire1")) {
			if (canJump) {
				rb.velocity = new Vector3 (0, 0, 0);
				canJump = false;
				jumpHeld = true;
				Invoke ("StopJump", jumpTime);
				int randy = Random.Range (0, jumps.Length);
				sound.PlayOneShot (jumps [randy]);
			}
		}
		if (Input.GetButtonUp ("Fire1")) {
			jumpHeld = false;
			CancelInvoke ("StopJump");
			jumpForce = 3;
		}


		if (inAir) {
			if (hasUmbrella) {
				if (swaangsLeft > 0) {
					if (!cantStab) {
						if (Input.GetButtonDown ("Fire2")) {
							velocityStore = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
							rb.isKinematic = true;
							isHolding = true;
							wallChecker.SetActive (true);
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

	void NotHolding(){
		rb.isKinematic = false;
		isHolding = false;

	}

	IEnumerator IncreaseLatDrag(){
		yield return new WaitForSeconds (0.1f);
		latDrag += dragIncr;
		if (latDrag < maxLatDrag-0.1f)
			StartCoroutine (IncreaseLatDrag ());
	}

	void StopJump(){
		jumpHeld = false;
		jumpForce = 1;
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
		if (isHolding && other.tag != "Blocks" && other.tag != "Snakey") {
			isHolding = false;
			rb.isKinematic = true;
			Invoke ("NotKinematic", 0.0001f);
			isFalling = true;
			CancelInvoke ("Drop");
		}


		if (other.tag == "Snakey") {
			transform.parent = GUTS.transform;

		}
	}
	void OnTriggerExit(Collider other){

		if (other.tag == "Snakey") {
			transform.parent = null;

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
