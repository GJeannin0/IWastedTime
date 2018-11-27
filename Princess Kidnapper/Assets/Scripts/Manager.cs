using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	[SerializeField]
	private bool isPaused = false;
	[SerializeField]
	private GameObject pauseMenu;
	[SerializeField]
	private MainMenu onPauseMenu;

	[SerializeField]
	private CheckPoint checkPoint;
	[SerializeField]
	private bool checkedCheckpoint;
	[SerializeField]
	private Vector3 checkpointPosition;

	[SerializeField]
	private int difficulty = 0;
	[SerializeField]
	private int maxDifficulty = 2;


	void Start()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name == "SecondStartScene")
		{
			checkedCheckpoint = false;
			isPaused = false;
		}

		if (SceneManager.GetActiveScene().name == "GameScene")
		{
			checkPoint = FindObjectOfType<CheckPoint>();
			if (checkPoint.GetCheckedCheckpoint() && !checkedCheckpoint)
			{
				checkedCheckpoint = true;
				checkpointPosition = checkPoint.transform.position;
			}
				if ((Input.GetButtonDown("Pause") || onPauseMenu.GetWillUnpause()) && isPaused)
			{
				onPauseMenu.SetWillUnpauseFalse();
				isPaused = false;
				pauseMenu.SetActive(false);
				Time.timeScale = 1.0f;
			}
			else
			{
				if (Input.GetButtonDown("Pause") && !isPaused)
				{
					isPaused = true;
					pauseMenu.SetActive(true);
					Time.timeScale = 0.0f;
				}
			}
		}

		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}
	}

	public bool GetIsPaused()
	{
		return isPaused;
	}

	public void SetDifficulty(int difficultyIndex)
	{
		if (difficultyIndex >= 0 && difficultyIndex <= maxDifficulty)
		{
			difficulty = difficultyIndex;
		}
		else
		{
			difficulty = 0;
		}
	}

	public int GetDifficulty()
	{
		return difficulty;
	}

	public bool GetCheckedCheckpoint()
	{
		return checkedCheckpoint;
	}

	public Vector3 GetCheckpointPosition()
	{
		return checkpointPosition;
	}

	public void LoadDeathScene()
	{
		SceneManager.LoadScene("DeathScene");
	}

	public void LoadVictoryScene()
	{
		SceneManager.LoadScene("VictoryScene");
	}
}
