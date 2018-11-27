using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;

	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip jumpSound;
	[SerializeField]
	private AudioClip dashSound;
	[SerializeField]
	private float dashSoundVolume = 2.0f;
	[SerializeField]
	private float jumpSoundVolume = 0.6f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			if (player.GetDoesJumpSound())
			{
				audioSource.PlayOneShot(jumpSound, jumpSoundVolume);
				player.DoesJumpSoundFalse();
			}

			if (Input.GetButtonDown("Fire2") && !player.GetDashOnCooldown() && !player.GetIsDashing())
			{
				audioSource.PlayOneShot(dashSound, dashSoundVolume);
			}
		}
	}
}
