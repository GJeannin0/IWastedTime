using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Animator anim;
	[SerializeField]
	private Player player;
	[SerializeField]
	private const float VERTICAL_AXIS_MINIMUM_TO_DOWN = -0.4f;

	[SerializeField]
	private bool isBumped = false;
	[SerializeField]
	private float bumpDuration = 3.0f;
	[SerializeField]
	private float bumpTime = 0.0f;
	[SerializeField]
	private bool doesBumpedSound = false;

	[SerializeField]
	private bool slash = false;
	[SerializeField]
	private float slashDuration = 0.16f;
	[SerializeField]
	private float slashTime = 0.0f;
	[SerializeField]
	private bool doesSlashSound = false;

	[SerializeField]
	private bool aimingDown = false;
	[SerializeField]
	private bool isSwinging = false;
	[SerializeField]
	private float delayBetweenSwings = 1.0f;
	[SerializeField]
	private float swingTime = 0.0f;

	[SerializeField]
	private bool isChargingShockwave = false;
	[SerializeField]
	private float timeToChargeShockwave = 2.0f;
	[SerializeField]
	private float timeChargingShockwave = 0.0f;
	[SerializeField]
	private float maximumChargeTime = 4.0f;
	[SerializeField]
	private float leftShockwaveAngle = 180.0f;
	[SerializeField]
	private float downwardRightShockwaveAngle = -45.0f;
	[SerializeField]
	private float downwardLeftShockwaveAngle = -135.0f;

	[SerializeField]
	private Shockwave shockwave;
	[SerializeField]
	private float positionOffset = 1.0f;

	[SerializeField]
	private ParticleSystem chargeParticles;
	[SerializeField]
	private ParticleSystem shockwaveReadyParticles;
	[SerializeField]
	private float minimumTimeChargingShockwave = 0.6f;
	[SerializeField]
	private float particlesLifeTime = 1.0f;


	void Start ()
	{
		anim = GetComponent<Animator>();
		gameManager = FindObjectOfType<Manager>();
	}
	
	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			IncrementTimers();

			UpdateShockwaveCharge();

			if (Input.GetButtonDown("Fire1") && !isSwinging && !isBumped)
			{
				if (Input.GetButton("Down") || Input.GetAxis("Vertical") <= VERTICAL_AXIS_MINIMUM_TO_DOWN)
				{
					aimingDown = true;
					anim.SetBool("aimingDown", true);
				}

				isSwinging = true;			
				slash = true;
				isChargingShockwave = true;
				anim.SetBool("isSwinging", true);
				doesSlashSound = true;
			}

			if (Input.GetButtonUp("Fire1"))
			{
				if (timeChargingShockwave >= timeToChargeShockwave)
				{
					if (Input.GetButton("Down") || Input.GetAxis("Vertical") <= VERTICAL_AXIS_MINIMUM_TO_DOWN)
					{
						aimingDown = true;
						anim.SetBool("aimingDown", true);
					}

					isSwinging = true;
					anim.SetBool("isSwinging", true);
					InstantiateShockwave();
				}

				isChargingShockwave = false;
				timeChargingShockwave = 0.0f;
			}
		}
	}

	private void UpdateShockwaveCharge()
	{
		if (timeChargingShockwave >= minimumTimeChargingShockwave)
		{
			chargeParticles.startLifetime = particlesLifeTime;

			if (timeChargingShockwave >= timeToChargeShockwave)
			{
				chargeParticles.startLifetime = 0.0f;
				shockwaveReadyParticles.startLifetime = particlesLifeTime;
			}
		}
		else
		{
			shockwaveReadyParticles.startLifetime = 0.0f;
			chargeParticles.startLifetime = 0.0f;
		}
	}

	private void IncrementTimers()
	{
		if (slash)
		{
			slashTime += 1.0f * Time.deltaTime;
			if (slashTime >= slashDuration)
			{
				slashTime = 0.0f;
				slash = false;
			}
		}

		if (isBumped)
		{
			bumpTime += 1.0f * Time.deltaTime;
			if (bumpTime >= bumpDuration)
			{
				bumpTime = 0.0f;
				isBumped = false;
				anim.SetBool("isBumped", isBumped);
			}
		}

		if (isChargingShockwave)
		{
			timeChargingShockwave += 1.0f * Time.deltaTime;
			if (timeChargingShockwave >= maximumChargeTime)
			{
				isChargingShockwave = false;
				timeChargingShockwave = 0.0f;
			}
		}

		if (isSwinging)
		{
			swingTime += (1.0f * Time.deltaTime);
			if (swingTime >= delayBetweenSwings)
			{
				swingTime = 0.0f;
				isSwinging = false;
				aimingDown = false;
				anim.SetBool("isSwinging", isSwinging);
				anim.SetBool("aimingDown", aimingDown);
			}
		}

	}

	private void InstantiateShockwave()
	{
		if (player.GetGoingLeft())
		{
			if (aimingDown)
			{
				Vector3 projPosition = new Vector3(gameObject.transform.position.x - positionOffset, gameObject.transform.position.y - positionOffset, gameObject.transform.position.z);
				Instantiate(shockwave, projPosition, Quaternion.Euler(0.0f,0.0f, downwardLeftShockwaveAngle));
			}
			else
			{
				Vector3 projPosition = new Vector3(gameObject.transform.position.x - positionOffset, gameObject.transform.position.y, gameObject.transform.position.z);
				Instantiate(shockwave, projPosition, Quaternion.Euler(Vector3.down * leftShockwaveAngle));
			}
		}
		else
		{
			if (aimingDown)
			{
				Vector3 projPosition = new Vector3(gameObject.transform.position.x + positionOffset, gameObject.transform.position.y - positionOffset, gameObject.transform.position.z);
				Instantiate(shockwave, projPosition, Quaternion.Euler(0.0f, 0.0f, downwardRightShockwaveAngle));
			}
			else
			{
				Vector3 projPosition = new Vector3(gameObject.transform.position.x + positionOffset, gameObject.transform.position.y, gameObject.transform.position.z);
				Instantiate(shockwave, projPosition, Quaternion.identity);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Princess" && slash)
		{
			gameManager.LoadDeathScene();
		}

		if (collision.gameObject.tag == "Enemy" && slash)
		{
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Dragon" && slash)
		{
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Shield" && slash)
		{
			slash = false;
			slashTime = 0.0f;

			isChargingShockwave = false;
			timeChargingShockwave = 0.0f;

			isBumped = true;
			anim.SetBool("isBumped", isBumped);
			doesBumpedSound = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Princess" && slash)
		{
			gameManager.LoadDeathScene();
		}

		if (collision.gameObject.tag == "Enemy" && slash)
		{
			Destroy(collision.gameObject);
		}
	}

	public bool GetIsBumped()
	{
		return isBumped;
	}
	
	public bool GetSlash()
	{
		return slash;
	}

	public bool GetIsChargingShockwave()
	{
		return isChargingShockwave;
	}

	public bool GetAimingDown()
	{
		return aimingDown;
	}

	public bool GetIsSwinging()
	{
		return isSwinging;
	}

	public float GetTimeChargingShockwave()
	{
		return timeChargingShockwave;
	}

	public bool GetDoesBumpedSound()
	{
		return doesBumpedSound;
	}

	public void DoesBumpedSoundFalse()
	{
		doesBumpedSound = false;
	}

	public bool GetDoesSlashSound()
	{
		return doesSlashSound;
	}

	public void DoesSlashSoundFalse()
	{
		doesSlashSound = false;
	}
}
