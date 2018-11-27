using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	private Manager gameManager;
	[SerializeField]
	private AudioMixer audioMixer;
	[SerializeField]
	private bool willUnpause = false;


	void Start()
	{
		gameManager = FindObjectOfType<Manager>();
	}

	public void StartGame()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SetVolume(float newVolume)
	{
		audioMixer.SetFloat("volume", newVolume);
	}

	public void SetEffectsVolume(float newVolume)
	{
		audioMixer.SetFloat("effectsVolume", newVolume);
	}

	public void SetMusicVolume(float newVolume)
	{
		audioMixer.SetFloat("musicVolume", newVolume);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("SecondStartScene");
	}

	public bool GetWillUnpause()
	{
		return willUnpause;
	}

	public void SetWillUnpauseTrue()
	{
		willUnpause = true;
	}

	public void SetWillUnpauseFalse()
	{
		willUnpause = false;
	}

	public void SetDifficulty(int difficultyIndex)
	{
		gameManager.SetDifficulty(difficultyIndex);
	}
}
