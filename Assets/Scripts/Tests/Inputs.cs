using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour {

	//Axis
	public static float x;
	public static float y;

	//Trigger Buttons
	public static float r;
	public static float l;

	//Buttons: A, B, X, Y, LB, RB
	public static bool[] fire = {false,false,false,false,false,false,false};

	void Update () {

		//Set the Axis
		x = Input.GetAxisRaw ("Horizontal");
		y = Input.GetAxisRaw ("Vertical");

		//Set the Triggers
		r = Input.GetAxisRaw ("RightTrigger");
		l = Input.GetAxisRaw ("LeftTrigger");

		//Set all of the Buttons
		for (int i = 1; i < 7; i++) {
			if (Input.GetButtonDown ("Fire"+i)) {
				fire [i] = true;
			}
			else {
				fire [i] = false;
			}

		}
	}
}
