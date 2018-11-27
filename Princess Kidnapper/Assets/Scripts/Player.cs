using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private Collider2D playerCollider;
	[SerializeField]
	private Rigidbody2D playerRigidbody2D;
	[SerializeField]
	private Sword sword;
	[SerializeField]
	private const float VERTICAL_AXIS_MINIMUM_TO_DOWN = -0.9f;

	[SerializeField]
	private GameObject playerCameraHolder;
	[SerializeField]
	private Vector3 cameraHeightDifference = new Vector3(0.0f, 6.0f, 0.0f);
	[SerializeField]
	private Vector3 cameraHorizontalDifference = new Vector3(10.0f, 0.0f, 0.0f);

	[SerializeField]
	private const float AXIS_MINIMUM_TURN = 0.001f;
	[SerializeField]
	private bool goingLeft = false;
	[SerializeField]
	private float basePlayerSpeed = 18.0f;
	[SerializeField]
	private float playerSpeed = 18.0f;
	[SerializeField]
	private float jumpSpeed = 25.0f;
	[SerializeField]
	private float fallSpeed = 1.6f;

	[SerializeField]
	private bool dashOnCooldown = false;
	[SerializeField]
	private bool isDashing = false;
	[SerializeField]
	private float dashCooldownTimer = 0.0f;
	[SerializeField]
	private float dashDurationTimer = 0.0f;
	[SerializeField]
	private float dashCooldown = 2.0f;
	[SerializeField]
	private float dashSpeed = 90.0f;
	[SerializeField]
	private float dashDuration = 0.20f;

	[SerializeField]
	private bool isJumping;
	[SerializeField]
	private bool doesJumpSound;
	[SerializeField]
	private Transform referenceRaycastCircleTransform;
	[SerializeField]
	private float raycastCircleRadius = 0.5f;
	[SerializeField]
	private LayerMask raycastJumpLayerMask;

	[SerializeField]
	private float minimumY = -30.0f;
	[SerializeField]
	private const float FALL_EPSILON = -0.01f;
	[SerializeField]
	private const float JUMP_EPSILON = 0.01f;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();

		if (gameManager.GetCheckedCheckpoint())
		{
			transform.position = gameManager.GetCheckpointPosition();
		}
	}

	void Update()
	{
		if (!gameManager.GetIsPaused())
		{
			MoveCamera();

			UpdateTimers();

			Flip();

			Collider2D collider = Physics2D.OverlapCircle(referenceRaycastCircleTransform.position, raycastCircleRadius, raycastJumpLayerMask);

			float horizontal = Input.GetAxis("Horizontal");
			Vector2 myVelocity = Vector2.right;

			Vector2 gravity = playerRigidbody2D.velocity;
			gravity.x = 0;

			if (collider != null && playerRigidbody2D.velocity.y < JUMP_EPSILON)
			{
				isJumping = false;
			}

			if (isJumping && playerRigidbody2D.velocity.y < FALL_EPSILON)
			{
				gravity.y -= fallSpeed;
			}

			if (Input.GetButtonDown("Jump") && collider != null && !isJumping)
			{
				doesJumpSound = true;
				gravity.y += jumpSpeed;
				isJumping = true;
			}

			if (sword.GetIsChargingShockwave())
			{
				playerSpeed /= 1.2f;
			}

			myVelocity.x *= horizontal;
			myVelocity.x *= playerSpeed;

			if (Input.GetButtonDown("Fire2") && !dashOnCooldown && !isDashing)
			{
				isDashing = true;
			}

			myVelocity += gravity;

			playerRigidbody2D.velocity = myVelocity;
		}
	}

	private void UpdateTimers()
	{
		if (isDashing)
		{
			playerSpeed = dashSpeed;
			dashDurationTimer += 1.0f * Time.deltaTime;
			if (dashDurationTimer >= dashDuration)
			{
				playerSpeed = basePlayerSpeed;
				dashDurationTimer = 0.0f;
				isDashing = false;
				dashOnCooldown = true;
			}
		}
		else
		{
			playerSpeed = basePlayerSpeed;
		}

		if (dashOnCooldown)
		{
			dashCooldownTimer += 1.0f * Time.deltaTime;

			if (dashCooldownTimer >= dashCooldown)
			{
				dashCooldownTimer = 0.0f;
				dashOnCooldown = false;
			}
		}

		if (transform.position.y < minimumY)
		{
			SceneManager.LoadScene("DeathScene");
		}
	}

	private void Flip()
	{
		if (Input.GetAxis("Horizontal") >= AXIS_MINIMUM_TURN && !(sword.GetIsSwinging()))
		{
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			goingLeft = false;
		}

		if (Input.GetAxis("Horizontal") <= -AXIS_MINIMUM_TURN && !(sword.GetIsSwinging()))
		{
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			goingLeft = true;
		}
	}

	private void MoveCamera()
	{
		if (Input.GetButton("Down") || Input.GetAxis("Vertical") <= VERTICAL_AXIS_MINIMUM_TO_DOWN)
		{
			playerCameraHolder.transform.position = -cameraHeightDifference + transform.position;
		}
		else
		{
			playerCameraHolder.transform.position = cameraHeightDifference + transform.position;
		}

		if (goingLeft)
		{
			playerCameraHolder.transform.position = -cameraHorizontalDifference + playerCameraHolder.transform.position;
		}
		else
		{
			playerCameraHolder.transform.position = cameraHorizontalDifference + playerCameraHolder.transform.position;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(referenceRaycastCircleTransform.position, raycastCircleRadius);
	}

	public bool GetDashOnCooldown()
	{
		return dashOnCooldown;
	}

	public bool GetGoingLeft()
	{
		return goingLeft;
	}

	public bool GetIsJumping()
	{
		return isJumping;
	}

	public bool GetIsDashing()
	{
		return isDashing;
	}

	public bool GetDoesJumpSound()
	{
		return doesJumpSound;
	}

	public void DoesJumpSoundFalse()
	{
		doesJumpSound = false;
	}
}
