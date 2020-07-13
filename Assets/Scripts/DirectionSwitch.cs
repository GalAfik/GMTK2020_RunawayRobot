using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class DirectionSwitch : MonoBehaviour
{
	private void OnMouseOver()
	{
		// Left click
		if (Input.GetMouseButtonDown(0))
		{
			// Rotate counterclockwise
			transform.Rotate(0, -90, 0);
		}

		// Right click
		if (Input.GetMouseButtonDown(1))
		{
			// Rotate clockwise
			transform.Rotate(0, 90, 0);
		}
	}
}
