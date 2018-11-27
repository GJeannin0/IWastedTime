using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	[SerializeField]
	private Player player;
	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Rigidbody2D FireBallRigidbody2D;

	[SerializeField]
	private float destructionTimer = 0.0f;
	[SerializeField]
	private float fireBallSpeed = 5.0f;
	[SerializeField]
	private float hardFireBallSpeed = 9.0f;
	[SerializeField]
	private float fireBallLifeTime = 5.0f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 1)	// if difficulty is hard
		{
			fireBallSpeed = hardFireBallSpeed;
		}
	}

	void Update ()
	{
		if (!gameManager.GetIsPaused())
		{
			LookAtPlayer();

			transform.localScale = new Vector3(fireBallLifeTime - destructionTimer, fireBallLifeTime - destructionTimer, 1.0f);

			if (destructionTimer <= fireBallLifeTime)
			{
				destructionTimer += 1f * Time.deltaTime;
			}
			else
			{
				destructionTimer = 0f;
				Destroy(gameObject);
			}

			transform.position += transform.right * fireBallSpeed * Time.deltaTime;
		}
	}

	private void LookAtPlayer()
	{
		Vector3 dir = player.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
