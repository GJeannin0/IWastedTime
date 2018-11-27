using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Peasant : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Rigidbody2D peasantRigidbody2D;

	[SerializeField]
	private float positionDifference = 0.0f;
	[SerializeField]
	private const float FLIP_EPSILON = 0.10f;
	[SerializeField]
	private float peasantSpeed = 3.0f;
	[SerializeField]
	private float activeRange = 40.0f;
	[SerializeField]
	private float hardPeasantSpeed = 10.0f;
	[SerializeField]
	private float extremePeasantSizeModifier = 2.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)	 // if difficulty is hard
		{
			peasantSpeed = hardPeasantSpeed;
			if (gameManager.GetDifficulty() >= 2)	 // if difficulty is extreme
			{
				transform.localScale *= extremePeasantSizeModifier;
			}
		}
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			positionDifference = (player.transform.position.x - transform.position.x);

			if (Math.Abs(positionDifference) <= activeRange)
			{
				if (positionDifference > FLIP_EPSILON)
				{
					transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
					peasantRigidbody2D.velocity = new Vector2(peasantSpeed, peasantRigidbody2D.velocity.y);
				}

				if (positionDifference < -FLIP_EPSILON)
				{
					transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
					peasantRigidbody2D.velocity = new Vector2(-peasantSpeed, peasantRigidbody2D.velocity.y);
				}
			}
		}
	}
}
