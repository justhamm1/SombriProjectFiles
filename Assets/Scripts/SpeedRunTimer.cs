using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SpeedRunTimer : MonoBehaviour {

	public static bool started;
	public Text currentText;
	public Text bestText;
	public float currentTime;
	public float storeTime;
	public int minutes;
	public int bestMinutes;
	public int bestSeconds;
	public float bestDecimals;

	void Update () {
		if (!started && SceneManager.GetActiveScene ().buildIndex == 1) {
			started = true;
			InvokeRepeating ("Timer", 0, 0.01f);
			storeTime = 0;
			currentTime = 0;
		}
		if (started && SceneManager.GetActiveScene ().buildIndex == 5) {
			started = false;
			CancelInvoke ("Timer");
			if (PlayerPrefs.GetFloat ("BestTime") == 0) {
				PlayerPrefs.SetFloat ("BestTime", storeTime);
			}

			if (PlayerPrefs.GetFloat ("BestTime") > storeTime) {
				PlayerPrefs.SetFloat ("BestTime", storeTime);			
			}
		}
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			currentTime = 0;
			minutes = 0;
			currentText.text = "Current Time: 00:00.00";
			CancelInvoke ("Timer");
		}

		bestMinutes = Mathf.FloorToInt (PlayerPrefs.GetFloat ("BestTime") / 60);
		bestSeconds =  Mathf.FloorToInt (PlayerPrefs.GetFloat ("BestTime")) - (60 * bestMinutes);
		bestDecimals = (float)System.Math.Round (PlayerPrefs.GetFloat ("BestTime"),2) - Mathf.FloorToInt(PlayerPrefs.GetFloat ("BestTime"));
		float secAndMill = bestSeconds + bestDecimals;
		if (bestMinutes > 10) {
			if (bestSeconds < 10) {
				bestText.text = "Best Time: 0" + bestMinutes + ":0" + secAndMill;
			} else
				bestText.text = "Best Time: 0" + bestMinutes + ":" + secAndMill;
		} else {
			if (bestSeconds < 10) {
				bestText.text = "Best Time: 0" + bestMinutes + ":0" + secAndMill;
			} else
				bestText.text = "Best Time: 0" + bestMinutes + ":" + secAndMill;
		}
	}

	void Timer(){
		if(SceneManager.GetActiveScene ().buildIndex != 5){
			storeTime += 0.01f;
			currentTime += 0.01f;
			storeTime = (float)System.Math.Round (storeTime, 2);
		}

		if (currentTime >= 60) {
			currentTime = 0;
			minutes++;
		}
		if (minutes > 0 && minutes < 10) {
			if (currentTime < 10) {
				currentText.text = "Current Time: 0" + minutes + ":0" + System.Math.Round (currentTime, 2);
			} else {
				currentText.text = "Current Time: 0" + minutes + ":" + System.Math.Round (currentTime, 2);
			}
		} else if (minutes >= 10) {
			if (currentTime < 10) {
				currentText.text = "Current Time: " + minutes + ":0" + System.Math.Round (currentTime, 2);
			} else {
				currentText.text = "Current Time: " + minutes + ":" + System.Math.Round (currentTime, 2);
			}
		} else if (currentTime < 10) {
			currentText.text = "Current Time: 0:0" + System.Math.Round (currentTime, 2);
		} else {
			currentText.text = "Current Time: 0:" + System.Math.Round (currentTime, 2);
		}
	}

}
