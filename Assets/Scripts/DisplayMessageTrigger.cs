using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessageTrigger : MonoBehaviour
{
	public Player Player;
	public Tutorial TutorialMessage;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == Player.gameObject)
		{
			// Show the relevant message
			TutorialMessage.gameObject.SetActive(true);

			// Destroy this object to avoid further collisions
			Destroy(gameObject);
		}
	}
}
