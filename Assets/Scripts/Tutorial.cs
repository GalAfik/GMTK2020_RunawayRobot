using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
	public GameManager GameManager;
	public TMP_Text[] Messages;
	private int CurrentMessage = 0;
	private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
		Animator = GetComponent<Animator>();
    }

	private void OnEnable()
	{
		// Lock the player
		GameManager.LockPlayer(true);
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Messages.Length > CurrentMessage + 1)
			{
				// Display the next message if it exists
				Messages[CurrentMessage].gameObject.SetActive(false);
				CurrentMessage++;
				Messages[CurrentMessage].gameObject.SetActive(true);
			}
			else
			{
				// Otherwise, dismiss the tutorial
				StartCoroutine(Exit());
			}
		}
    }

	IEnumerator Exit()
	{
		// play the ending animation
		Animator.SetTrigger("Exit");
		GameManager.StartLevel();

		yield return new WaitForSeconds(1.5f);
		// Unlock the player
		GameManager.LockPlayer(false);

		// Deactivate this tutorial
		Destroy(gameObject);
	}
}
