using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour {

	[SerializeField]
	private Player player;
	[SerializeField]
	private bool checkedCheckpoint = false;


	void Start ()
	{
		player = FindObjectOfType<Player>();
	}

	void Update()
	{
		if (!checkedCheckpoint)
		{
			if (player.transform.position.x > transform.position.x)
			{
				checkedCheckpoint = true;
			}
		}
	}

	public bool GetCheckedCheckpoint()
	{
		return checkedCheckpoint;
	}
}
