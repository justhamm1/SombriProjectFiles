using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEtactive : MonoBehaviour {

	public GameObject goejwi;
	public bool deactive;
	public float time;

	void Start () {
		Invoke ("Go", time);

	}

	void Go () {
		if(!deactive)
		goejwi.SetActive (true);
		else goejwi.SetActive (false);
	}
}
