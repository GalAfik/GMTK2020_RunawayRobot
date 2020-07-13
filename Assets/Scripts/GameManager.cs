using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Player Player;
	public Robot[] Robots;
	public Canvas UI;
	public bool IsThereATutorial;
	private Animator UIAnimator;
	private bool ControlsEnabled = true;

	private void Start()
	{
		UIAnimator = UI.GetComponent<Animator>();

		// If there is no tutorial for this level, start the level
		if (!IsThereATutorial)
		{
			StartLevel();
		}
	}

	// Update is called once per frame
	void Update()
    {
		// Check if all robots have reached the goal
		bool levelCompleted = true;
		foreach (Robot robot in Robots)
		{
			if (robot.IsReachedGoal() == false) levelCompleted = false;
		}
		if (levelCompleted) StartCoroutine(LoadNextLevel());

		// Handle global controls
		HandleGlobalControls();
	}

	void HandleGlobalControls()
	{
		if (ControlsEnabled)
		{
			if (Input.GetButtonDown("Restart"))
			{
				// Reload the current scene
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			if (Input.GetButtonDown("Quit"))
			{
				Application.Quit();
			}
		}
	}

	public void DisableGlobalControls()
	{
		ControlsEnabled = false;
	}

	public void StartLevel()
	{
		foreach (Robot robot in Robots)
		{
			// Start the robot countdown
			StartCoroutine(robot.TimerCountdown());
		}
	}

	public IEnumerator LoadNextLevel()
	{
		// Display the congrats message
		UIAnimator.SetTrigger("Transition");
		yield return new WaitForSeconds(4f);
		// Load the next scene in the build index
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LockPlayer(bool locked)
	{
		Player?.SetLocked(locked);
	}
}
