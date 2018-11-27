using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireMageHands : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private FireBall fireBall;

	[SerializeField]
	private bool readyToShoot = true;
	[SerializeField]
	private float shotTimer = 0.0f;
	[SerializeField]
	private float shootingRange = 20.0f;
	[SerializeField]
	private float hardShootingRange = 40.0f;
	[SerializeField]
	private float shotCooldown = 3.0f;
	[SerializeField]
	private float extremeShotCooldown = 1.5f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)   // if difficulty is hard
		{
			shootingRange = hardShootingRange;

			if (gameManager.GetDifficulty() >= 2)	// if difficulty is extreme
			{
				shotCooldown = extremeShotCooldown;
			}
		}
	}
	
	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			LookAtPlayer();

			if (shotTimer < shotCooldown)
			{
				shotTimer += 1.0f * Time.deltaTime;
			}
			else
			{
				readyToShoot = true;
			}

			if (readyToShoot && Math.Abs(player.transform.position.x - transform.position.x) <= shootingRange)
			{
				Instantiate(fireBall, transform.position, transform.rotation);
				shotTimer = 0.0f;
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
