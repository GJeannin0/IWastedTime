using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour {

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
			gameManager.LoadVictoryScene();
		}
	}		
}
