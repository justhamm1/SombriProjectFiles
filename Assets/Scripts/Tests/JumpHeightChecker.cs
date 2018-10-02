using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHeightChecker : MonoBehaviour {

	public TextMesh currentText;
	public TextMesh maxText;
	public double maxHeight;
	public double currentHeight;


	void Start () {
		
	}
	

	void Update () {

		maxText.text = "Max Height: " + maxHeight;
		currentText.text = "Height: " + currentHeight;

		if (Input.GetButtonDown ("Fire1")) {
			maxHeight = 0;
		}

	}

	void OnTriggerStay(Collider other){
		
		if (other.gameObject != null) {
			currentHeight = System.Math.Round(other.gameObject.transform.position.y - 1.5f, 3);
		}
		if (maxHeight < currentHeight) {
			maxHeight = currentHeight;
		}

	}
}
