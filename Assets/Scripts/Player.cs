using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameManager GameManager;

	public float MoveSpeed = 1.2f;
	private Vector3 MoveVector;
	private Vector3 HorizontalMoveVector;

	private Animator Animator;
	private CharacterController CharacterController;

	public bool Locked = true;
	public void SetLocked(bool locked) { Locked = locked; }
	public bool IsLocked() { return Locked; }

	private bool Moved = false;
	public bool HasMoved() { return Moved; }

	private void Start()
	{
		Animator = GetComponentInChildren<Animator>();
		CharacterController = GetComponentInChildren<CharacterController>();
	}

	// Update is called once per frame
	void Update()
    {
		HandleMovementInput();
		HandleAnimation();
    }

	void HandleMovementInput()
	{
		if (!Locked)
		{
			MoveVector = new Vector3( Input.GetAxisRaw("Horizontal"), Physics.gravity.y, Input.GetAxisRaw("Vertical") );
			HorizontalMoveVector = new Vector3(MoveVector.normalized.x, 0, MoveVector.normalized.z);
			CharacterController.Move(MoveVector * MoveSpeed * Time.deltaTime);
		
			if (HorizontalMoveVector != Vector3.zero) transform.forward = HorizontalMoveVector;
			if (HorizontalMoveVector.magnitude > 0.1f) Moved = true;
		}
	}

	void HandleAnimation()
	{
		if (HorizontalMoveVector.magnitude > 0.1f) Animator.SetBool("Walk Forward", true);
		else Animator.SetBool("Walk Forward", false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Goal"))
		{
			// Start the end of the game
			StartCoroutine(GameManager.LoadNextLevel());
		}
	}
}
