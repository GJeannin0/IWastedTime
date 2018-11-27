using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bow : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private Arrow arrow;

	[SerializeField]
	private bool readyToShoot = true;
	[SerializeField]
	private float shotsTimer = 0.0f;
	[SerializeField]
	private float shootingRange = 20.0f;
	[SerializeField]
	private float hardShootingRange = 40.0f;
	[SerializeField]
	private float shotCooldown = 2.0f;
	[SerializeField]
	private float ExtremeShotCooldown = 0.9f;
	

	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)	// if difficulty is hard
		{
			shootingRange = hardShootingRange;

			if (gameManager.GetDifficulty() >= 2)   // if difficulty is extreme
			{
				shotCooldown = ExtremeShotCooldown;
			}
		}
	}

	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			LookAtPlayer();

			if (shotsTimer < shotCooldown)
			{
				shotsTimer += 1.0f * Time.deltaTime;
			}
			else
			{
				readyToShoot = true;
			}

			if (readyToShoot && Math.Abs(player.transform.position.x - transform.position.x) <= shootingRange)
			{
				anim.SetTrigger("isShooting");
				Instantiate(arrow, transform.position, transform.rotation);
				shotsTimer = 0.0f;
				readyToShoot = false;
			}
		}
	}

	private void LookAtPlayer()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
