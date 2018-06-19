using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {

	public GameObject speedRunner;
	public GameObject fade;
	public static bool speedRun;

	public void PressStartButton(){
		Time.timeScale = 1;
		Invoke ("LoadFade", 2);
		fade.SetActive (true);
		this.enabled = false;
		Cursor.visible = true;
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void UnPause(){
		Time.timeScale = 1;
		PlayerContorller.isPaused = false;
	}

	public void StartSpeedRun(){
		Time.timeScale = 1;
		speedRun = true;
		speedRunner.SetActive (true);
		Invoke ("LoadFade", 2);
		fade.SetActive (true);
		this.enabled = false;

	}
	public void LoadFade(){

		SceneManager.LoadScene(1);
	}

	public void MainMenu(){
		Time.timeScale = 1;
		PlayerContorller.isPaused = false;
		SceneManager.LoadScene (0);
		SpeedRunTimer.started = false;
	}
}
