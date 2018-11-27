using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bowman : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private GameObject originalPosition;
	[SerializeField]
	private Rigidbody2D bowmanRigidbody2D;

	[SerializeField]
	private float playerPositionDifference = 0.0f;
	[SerializeField]
	private float originalPositionDifference = 0.0f;
	[SerializeField]
	private const float FLIP_EPSILON = 0.05f;
	[SerializeField]
	private bool outOfPosition = false;
	[SerializeField]
	private float activeRange = 40.0f;
	[SerializeField]
	private float positionLimit = 3.0f;
	[SerializeField]
	private float bowmanSpeed = 1.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			originalPositionDifference = (originalPosition.transform.position.x - transform.position.x);

			playerPositionDifference = (player.transform.position.x - transform.position.x);

			if (Math.Abs(playerPositionDifference) <= activeRange)
			{
				Flip();

				if (Math.Abs(originalPositionDifference) < positionLimit)
				{
					outOfPosition = false;
				}
				else
				{
					outOfPosition = true;
				}

				if (outOfPosition)
				{
					if (originalPositionDifference > FLIP_EPSILON)
					{
						bowmanRigidbody2D.velocity = new Vector2(bowmanSpeed, bowmanRigidbody2D.velocity.y);
					}

					if (originalPositionDifference < -FLIP_EPSILON)
					{
						bowmanRigidbody2D.velocity = new Vector2(-bowmanSpeed, bowmanRigidbody2D.velocity.y);
					}
				}
				else
				{
					if (playerPositionDifference < -FLIP_EPSILON)
					{
						bowmanRigidbody2D.velocity = new Vector2(bowmanSpeed, bowmanRigidbody2D.velocity.y);
					}

					if (playerPositionDifference > FLIP_EPSILON)
					{
						bowmanRigidbody2D.velocity = new Vector2(-bowmanSpeed, bowmanRigidbody2D.velocity.y);
					}
				}
			}
		}
	}

	private void Flip()
	{
		if (playerPositionDifference > FLIP_EPSILON)
		{
			transform.rotation = Quaternion.Euler(0.0f, 180.0f, 1.0f);
		}

		if (playerPositionDifference < -FLIP_EPSILON)
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 1.0f);
		}
	}
}

