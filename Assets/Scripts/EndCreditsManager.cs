using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCreditsManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		// Exit the application when the player presses the Esc key
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			print("Escape...");
			Application.Quit();
		}
    }
}
