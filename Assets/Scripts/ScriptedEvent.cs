using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedEvent : MonoBehaviour
{
	public int EventNumber = 1;

	public GameManager GameManager;
	public GameObject Robot;
	public Player Player;
	public GameObject Goal;
	public Canvas UI;
	public AudioClip BackgroundMusic;

	public ScriptedObject[] ScriptedObjects;
	public Tutorial[] TutorialMessages;

    IEnumerator StartEvent()
	{
		UI?.GetComponent<Animator>().SetTrigger("Fade Controls");
		GameManager.DisableGlobalControls();
		yield return new WaitForSeconds(1);

		switch (EventNumber)
		{
			case 1:
				TutorialMessages[0].gameObject.SetActive(true);
				while (Player.IsLocked())
				{
					yield return new WaitForEndOfFrame();
				}

				yield return new WaitForSeconds(2);

				StartCoroutine(GameManager.LoadNextLevel());
				break;

			case 2:
				TutorialMessages[0].gameObject.SetActive(true);
				while (Player.IsLocked())
				{
					yield return new WaitForEndOfFrame();
				}

				// Fade out the background music
				AudioSource Audio = FindObjectOfType<AudioSource>();
				if (Audio != null)
				{
					Audio.GetComponent<Animator>().SetTrigger("Fade Out");
				}

				yield return new WaitForSeconds(2);

				TutorialMessages[1].gameObject.SetActive(true);
				while (Player.IsLocked())
				{
					yield return new WaitForEndOfFrame();
				}

				// Change the background music
				if (Audio != null)
				{
					Audio.GetComponent<Animator>().SetTrigger("Fade In");
					Audio.volume = .07f;
					Audio.clip = BackgroundMusic;
					Audio.Play();
				}

				yield return new WaitForSeconds(2);

				Robot?.SetActive(false);
				Goal?.SetActive(false);
				Player?.gameObject.SetActive(true);

				// Set up the camera to follow the player
				Camera.main.GetComponent<FollowCamera>().enabled = true;
				Camera.main.GetComponent<FollowCamera>().ObjectToFollow = Player.transform;
				Player?.SetLocked(false);


				yield return new WaitForSeconds(8);
				if (Player.HasMoved() == false)
				{
					TutorialMessages[2].gameObject.SetActive(true);
					while (Player.IsLocked())
					{
						yield return new WaitForEndOfFrame();
					}
				}

				break;

			case 3:
				ScriptedObjects[0].StartAction();
				TutorialMessages[0].gameObject.SetActive(true);
				while (Player.IsLocked())
				{
					yield return new WaitForEndOfFrame();
				}

				yield return new WaitForSeconds(1);

				ScriptedObjects[1].StartAction();

				break;

			default:
				break;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Robot)
		{
			StartCoroutine(StartEvent());
		}
	}
}
