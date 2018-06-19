using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour {

	public AudioClip[] sounds;
	AudioSource sound;
	public float repeatTime;

	void Start () {
		
		sound = GetComponent<AudioSource> ();
		int randy = Random.Range (0, sounds.Length);
		sound.PlayOneShot (sounds [randy]);
		InvokeRepeating ("PlaySound", 0, repeatTime);

	}
	void PlaySound(){
		int randy = Random.Range (0, sounds.Length);
		sound.PlayOneShot (sounds [randy]);
	}
	void OnDisabled(){
		CancelInvoke ();
	}
}
