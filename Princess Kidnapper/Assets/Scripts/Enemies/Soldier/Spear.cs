using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Player player;
	[SerializeField]
	private Soldier soldier;

	[SerializeField]
	private float minimumAimingHeight = 3.5f;
	[SerializeField]
	private float aimAngle = 60.0f;
	[SerializeField]
	private Vector3 extremeSpearScale = new Vector3(3.0f, 1.0f, 1.0f);


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
		player = FindObjectOfType<Player>();

		if (gameManager.GetDifficulty() >= 2)	// if difficulty is extreme
		{
			transform.localScale = extremeSpearScale;
		}
	}

	void Update ()
	{
		if (player.transform.position.y - transform.position.y > minimumAimingHeight)
		{
			if (soldier.GetGoingLeft())
			{
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, -aimAngle);
			}
			else
			{
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);
			}
		}
		else
		{
			if (player.transform.position.y - transform.position.y < -minimumAimingHeight)
			{
				if (soldier.GetGoingLeft())
				{
					transform.rotation = Quaternion.Euler(0.0f, 0.0f, aimAngle);
				}
				else
				{
					transform.rotation = Quaternion.Euler(0.0f, 0.0f, -aimAngle);
				}
			}
			else
			{
				transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
			}
		}
	}
}
