using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour {

	public int nextScene;
	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			SceneManager.LoadScene (nextScene);

		}
	}
}
