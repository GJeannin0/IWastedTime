using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireMouth : MonoBehaviour
{

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private FireBreath fireBreath;

	[SerializeField]
	private float breathCooldownTimer = 0.0f;
	[SerializeField]
	private float breathDurationTimer = 0.0f;
	[SerializeField]
	private const float FLIP_EPSILON = 0.05f;
	[SerializeField]
	private bool readyToBreathe = false;
	[SerializeField]
	private bool isBreathing = false;

	[SerializeField]
	private float breathTriggerRange = 10.0f;
	[SerializeField]
	private float hardBreathRange = 20.0f;
	[SerializeField]
	private float breathCooldown = 4.0f;
	[SerializeField]
	private float extremeBreathCooldown = 2.0f;
	[SerializeField]
	private float breathDuration = 3.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)   // if difficulty is hard
		{
			breathTriggerRange = hardBreathRange;

			if (gameManager.GetDifficulty() >= 2)   // if difficulty is extreme
			{
				breathCooldown = extremeBreathCooldown;
			}
		}
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			LookAtPlayer();

			if (player.transform.position.x - transform.position.x < -FLIP_EPSILON)
			{
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}

			if (player.transform.position.x - transform.position.x > FLIP_EPSILON)
			{
				transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
			}

			if (breathCooldownTimer < breathCooldown)
			{
				breathCooldownTimer += 1.0f * Time.deltaTime;
			}
			else
			{
				readyToBreathe = true;
			}

			if (readyToBreathe && Math.Abs(player.transform.position.x - transform.position.x) <= breathTriggerRange)
			{
				isBreathing = true;
				readyToBreathe = false;
				breathCooldownTimer = 0.0f;
				anim.SetBool("OpenMouth", true);
			}

			if (isBreathing)
			{
				if (breathDurationTimer < breathDuration)
				{
					breathDurationTimer += 1.0f * Time.deltaTime;
					Instantiate(fireBreath, transform.position, transform.rotation);
				}
				else
				{
					isBreathing = false;
					breathDurationTimer = 0.0f;
					breathCooldownTimer = 0.0f;
					anim.SetBool("OpenMouth", false);
				}
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
