using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCorpse : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;

	[SerializeField]
	private Rigidbody2D dragonCorpseRigidbody2D;
	[SerializeField]
	private float minimumY = -30.0f;
	[SerializeField]
	private Vector2 initialFallSpeed = new Vector2(2.0f, 6.0f);
	

	void Start ()
	{
		dragonCorpseRigidbody2D.velocity = initialFallSpeed;
	}

	void Update()
	{
		if (transform.position.y < minimumY)
		{
			Destroy(gameObject);
		}
	}
}
