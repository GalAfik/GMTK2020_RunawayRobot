using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class FakeSwitch : MonoBehaviour
{
	public GameObject Arrows;

	private void OnMouseOver()
	{
		// Left click
		if (Input.GetMouseButtonDown(0))
		{
			// Rotate counterclockwise
			Arrows.transform.Rotate(0, 0, 90);
		}

		// Right click
		if (Input.GetMouseButtonDown(1))
		{
			// Rotate clockwise
			Arrows.transform.Rotate(0, 0, -90);
		}
	}
}
