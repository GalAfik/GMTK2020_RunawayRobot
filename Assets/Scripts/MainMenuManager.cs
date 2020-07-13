using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public string FirstLevel = "Level0";
	public string Webpage = "http://www.galafik.tech";
	public Canvas UI;
	private Animator UIAnimator;
	public AudioSource BackgroundMusic;

	private void Start()
	{
		UIAnimator = UI.GetComponent<Animator>();
	}

	public void StartGame()
	{
		// Load the first level
		UIAnimator.SetTrigger("Start Game");
		StartCoroutine(StartGameDelay());
	}

	IEnumerator StartGameDelay()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(FirstLevel);
	}

	public void MuteMusic()
	{
		if (BackgroundMusic != null) BackgroundMusic.mute = !BackgroundMusic.mute;
	}

	public void Exit()
	{
		// Quit out of the game
		Application.Quit();
	}

	public void Website()
	{
		// Go to my website
		Application.OpenURL(Webpage);
	}
}
