using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChecker : MonoBehaviour {

	public PlayerContorller contorller;
	public Transform spot;

	void Start () {
		
	}
	

	void Update () {
		transform.position = spot.position;
	}

	void OnTriggerStay(Collider other){

		if (other.tag == "Blocks" && !contorller.isHolding) {
			contorller.cantStab = true;
		}

	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Blocks") {
			contorller.cantStab = false;

		}
	}

}
