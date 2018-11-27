using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSound : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Sword sword;
	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip swordSlash;
	[SerializeField]
	private float swordSlashSoundVolume = 1.0f;
	[SerializeField]
	private AudioClip swordBump;
	[SerializeField]
	private float swordBumpSoundVolume = 1.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			if (sword.GetDoesSlashSound())
			{
				audioSource.PlayOneShot(swordSlash, swordSlashSoundVolume);
				sword.DoesSlashSoundFalse();
			}

			if (sword.GetDoesBumpedSound())
			{
				audioSource.PlayOneShot(swordBump, swordBumpSoundVolume);
				sword.DoesBumpedSoundFalse();
			}
		}
	}
}
