using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Rigidbody2D shockwaveRigidbody2D;

	[SerializeField]
	private float destructionTimer = 0.0f;
	[SerializeField]
	private float shockwaveSpeed = 20.0f;
	[SerializeField]
	private float shockwaveLifeTime = 2.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
	}

	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			if (destructionTimer <= shockwaveLifeTime)
			{
				destructionTimer += 1f * Time.deltaTime;
			}
			else
			{
				destructionTimer = 0f;
				Destroy(gameObject);
			}

			transform.position += transform.right * shockwaveSpeed * Time.deltaTime;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Princess")
		{
			gameManager.LoadDeathScene();
		}

		if (collision.gameObject.tag == "Enemy")
		{
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Dragon")
		{
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.tag == "Shield")
		{
			Destroy(gameObject);
		}
	}
}
