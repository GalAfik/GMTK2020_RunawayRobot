using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedObject : MonoBehaviour
{
	public float MoveSpeed = 2f;
	public Transform TargetPosition;

	private bool Active = false;

    // Start is called before the first frame update
    void Start()
    {
		TargetPosition.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
		if (Active && TargetPosition != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition.position, MoveSpeed * Time.deltaTime);
		}
    }

	public void StartAction()
	{
		Active = true;
	}
}
