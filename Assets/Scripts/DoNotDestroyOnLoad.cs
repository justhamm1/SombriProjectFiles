using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour {

	public static bool active = false;

	void Awake () {
		
		if (!active) {
			DontDestroyOnLoad (gameObject);
			active = true;
		}
	}

}
