using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Rigidbody2D arrowRigidbody2D;

	[SerializeField]
	private float destructionTimer = 0.0f;
	[SerializeField]
	private float arrowSpeed = 50.0f;
	[SerializeField]
	private float arrowLifeTime = 2.0f;


	void Start()
	{
		arrowRigidbody2D.velocity = transform.right * arrowSpeed;
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			if (destructionTimer <= arrowLifeTime)
			{
				destructionTimer += 1f * Time.deltaTime;
			}
			else
			{
				destructionTimer = 0f;
				Destroy(gameObject);
			}

			transform.right = arrowRigidbody2D.velocity;
		}
	}
}