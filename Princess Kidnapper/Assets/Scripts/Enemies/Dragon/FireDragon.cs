using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragon : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private GameObject originalPosition;

	[SerializeField]
	private float originalPositionDifference = 0.0f;
	[SerializeField]
	private float positionLimit = 5.0f;

	[SerializeField]
	private DragonCorpse dragonCorpse;
	[SerializeField]
	private bool isQuitting = false;

	[SerializeField]
	private const float FLIP_EPSILON = 0.05f;
	[SerializeField]
	private Rigidbody2D fireDragonRigidbody2D;
	[SerializeField]
	private float playerPositionDifference = 0.0f;
	[SerializeField]
	private float fireDragonSpeed = 5.0f;
	

	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();
		fireDragonRigidbody2D.velocity = new Vector2(0, fireDragonSpeed);
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			playerPositionDifference = (player.transform.position.x - transform.position.x);

			originalPositionDifference = (originalPosition.transform.position.y - transform.position.y);

			if (originalPositionDifference > positionLimit)
			{
				fireDragonRigidbody2D.velocity = new Vector2(0, fireDragonSpeed);
			}

			if (originalPositionDifference < -positionLimit)
			{
				fireDragonRigidbody2D.velocity = new Vector2(0, -fireDragonSpeed);
			}

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

	void OnApplicationQuit()
	{
		isQuitting = true;
	}

	void OnDestroy()
	{
		if (!isQuitting)
		{
			Instantiate(dragonCorpse, transform.position, transform.rotation);
		}
	}
}

