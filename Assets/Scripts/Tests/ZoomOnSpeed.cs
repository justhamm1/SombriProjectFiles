using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnSpeed : MonoBehaviour {

	Cinemachine.CinemachineFollowZoom followZoom;
	public GameObject player;
	Rigidbody playerRB;
	DirectionalLaunchControl playerLaunchController;
	public float playerVelocity;

	void Start () {
		followZoom = GetComponent<Cinemachine.CinemachineFollowZoom> ();
		playerRB = player.GetComponent<Rigidbody> ();
		playerLaunchController = player.GetComponent<DirectionalLaunchControl> ();
	}


	void Update () {
		playerVelocity = Mathf.Abs(playerRB.velocity.x) + Mathf.Abs(playerRB.velocity.y);

		if (followZoom.m_Width < 36) {
			
			if (playerLaunchController.canFling) {
				followZoom.m_Width = playerLaunchController.rotationSpeed + 20;
			}
			else
			followZoom.m_Width = playerVelocity*1.2f + 15;
		}

		if (followZoom.m_Width >= 36) {
			followZoom.m_Width = 35;
		}
		//if(playerVelocity < 1)
		//followZoom.m_Width = 15;
	}
}
