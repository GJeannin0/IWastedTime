using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Rigidbody2D fireMageRigidbody2D;
	[SerializeField]
	private float playerPositionDifference = 0.0f;
	[SerializeField]
	private const float FLIP_EPSILON = 0.05f;


	void Start ()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();
	}
	
	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			playerPositionDifference = (player.transform.position.x - transform.position.x);

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
}
