using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Soldier : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private GameObject originalPosition;
	[SerializeField]
	private Rigidbody2D soldierRigidbody2D;

	[SerializeField]
	private float playerPositionDifference = 0.0f;
	[SerializeField]
	private float originalPositionDifference = 0.0f;
	[SerializeField]
	private bool goingLeft = true;
	[SerializeField]
	private bool outOfPosition = false;
	[SerializeField]
	private const float FLIP_EPSILON = 0.05f;
	[SerializeField]
	private float activeRange = 40.0f;
	[SerializeField]
	private float soldierSpeed = 2.0f;
	[SerializeField]
	private float hardSoldierSpeed = 10.0f;
	[SerializeField]
	private float positionLimit = 5.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)	// if difficulty is hard
		{
			soldierSpeed = hardSoldierSpeed;
		}
	}

	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			playerPositionDifference = (player.transform.position.x - transform.position.x);

			if (Math.Abs(playerPositionDifference) <= activeRange)
			{
				originalPositionDifference = (originalPosition.transform.position.x - transform.position.x);

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
						soldierRigidbody2D.velocity = new Vector2(soldierSpeed, soldierRigidbody2D.velocity.y);
					}

					if (originalPositionDifference < -FLIP_EPSILON)
					{
						soldierRigidbody2D.velocity = new Vector2(-soldierSpeed, soldierRigidbody2D.velocity.y);
					}
				}
				else
				{
					if (playerPositionDifference > FLIP_EPSILON)
					{
						soldierRigidbody2D.velocity = new Vector2(soldierSpeed, soldierRigidbody2D.velocity.y);
					}

					if (playerPositionDifference < -FLIP_EPSILON)
					{
						soldierRigidbody2D.velocity = new Vector2(-soldierSpeed, soldierRigidbody2D.velocity.y);
					}
				}
			}
		}
	}

	private void Flip()
	{
		if (playerPositionDifference > FLIP_EPSILON)
		{
			goingLeft = false;
			transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
		}

		if (playerPositionDifference < -FLIP_EPSILON)
		{
			goingLeft = true;
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); ;
		}
	}

	public bool GetGoingLeft()
	{
		return goingLeft;
	}
}
