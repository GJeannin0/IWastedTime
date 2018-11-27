using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Rigidbody2D FireBreathRigidbody2D;

	[SerializeField]
	private float destructionTimer = 0.0f;
	[SerializeField]
	private float fireBreathSpeed = 10.0f;
	[SerializeField]
	private float fireBreathLifeTime = 1.2f;
	[SerializeField]
	private float hardFireBreathLifeTime = 1.7f;
	[SerializeField]
	private float scaleImprover = 1.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();

		if (gameManager.GetDifficulty() >= 1)
		{
			fireBreathLifeTime = hardFireBreathLifeTime;
		}
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			transform.localScale = new Vector3(fireBreathLifeTime - destructionTimer + scaleImprover, fireBreathLifeTime - destructionTimer + scaleImprover, 1.0f);

			if (destructionTimer <= fireBreathLifeTime)
			{
				destructionTimer += 1f * Time.deltaTime;
			}
			else
			{
				destructionTimer = 0f;
				Destroy(gameObject);
			}

			transform.position += transform.right * fireBreathSpeed * Time.deltaTime;
		}
	}
}
