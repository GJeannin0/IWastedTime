using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShockwaveSound : MonoBehaviour {

	[SerializeField]
	private Sword sword;

	[SerializeField]
	private AudioSource audioSource;
	[SerializeField]
	private AudioClip shockwaveCharging;
	[SerializeField]
	private bool startedCharging = false;
	[SerializeField]
	private float shockwaveChargingVolume = 0.5f;


	void Update()
	{
		if (sword.GetIsChargingShockwave() && !startedCharging)
		{
			audioSource.PlayOneShot(shockwaveCharging, shockwaveChargingVolume);
			startedCharging = true;
		}

		if (!sword.GetIsChargingShockwave())
		{
			audioSource.Stop();
			startedCharging = false;
		}
	}
}
