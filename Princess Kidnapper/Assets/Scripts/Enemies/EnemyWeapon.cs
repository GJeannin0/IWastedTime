using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			gameManager.LoadDeathScene();
		}
	}
}