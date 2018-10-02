using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numberer : MonoBehaviour {

	public TextMesh[] numbers;

	void Start () {
		
	}
	

	void Update () {

		for (int i = 0; i < numbers.Length; i ++) {

			numbers [i].text = "" + i * 2;

		}

	}
}
