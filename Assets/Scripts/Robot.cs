using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Robot : MonoBehaviour
{
	public GameObject TimerComponent;
	public TMP_Text TimerLabel;
	public int TimerDelay = 3;
	private int CurrentTimer;

	public float MoveSpeed = 5f;
	public Transform MoveTarget;
	private Vector3 MoveDirection;
	private bool Moving = false;

	public LayerMask CollidableObjects;
	public LayerMask TriggerObjects;

	private Animator RobotAnimator;

	private bool ReachedGoal = false;
	public bool IsReachedGoal() { return ReachedGoal; }

	// Start is called before the first frame update
	void Start()
    {
		// Decouple the move target object from the robot
		MoveTarget.parent = null;

		// Reset the move direction
		MoveDirection = transform.forward;

		// Get the Animator
		RobotAnimator = GetComponentInChildren<Animator>();

		// Start the timer
		CurrentTimer = TimerDelay;
	}

	public IEnumerator TimerCountdown()
	{
		// Set the timer label
		TimerLabel.text = CurrentTimer.ToString("D2");

		// Small delay before starting the countdown
		yield return new WaitForSeconds(1);

		while (CurrentTimer > 0)
		{
			// Set the timer label
			TimerLabel.text = CurrentTimer.ToString("D2");
			// Decrease the timer
			yield return new WaitForSeconds(1);
			CurrentTimer--;
		}
		// Set the timer label
		TimerLabel.text = "GO";
		yield return new WaitForSeconds(1);

		// Remove the timer and start the robot
		TimerComponent?.SetActive(false);
		Moving = true;
	}

    // Update is called once per frame
    void Update()
    {
		// Move toward the move target
		transform.position = Vector3.MoveTowards(transform.position, MoveTarget.position, MoveSpeed * Time.deltaTime);

		// When the robot is near enough to the move target, move the move target in the current movement direction
		if (Vector3.Distance(transform.position, MoveTarget.position) <= .05f)
		{
			// Check if the robot is on top of a direction switch
			Collider[] hits = Physics.OverlapSphere(transform.position, .1f, TriggerObjects, QueryTriggerInteraction.Collide);
			if (hits.Length > 0)
			{
				foreach (Collider hit in hits)
				{
					// Check for collisions with a switch trigger
					if (hit.CompareTag("DirectionSwitch"))
					{
						// Change the movement direction to the forward direction of the switch
						MoveDirection = hit.transform.forward;
					}

					// Check for collisions with a bounce cube
					if (hit.CompareTag("BounceCube"))
					{
						// Reverse the movement direction
						MoveDirection = -MoveDirection;
					}

					// Check for collisions with the level goal
					if (hit.CompareTag("Goal"))
					{
						Moving = false;
						ReachedGoal = true;
					}
				}
			}

			// Check if the position the robot is moving towards is free
			Collider[] targetHits = Physics.OverlapSphere(MoveTarget.position + MoveDirection, .1f, CollidableObjects, QueryTriggerInteraction.Ignore);
			// If the position is free, move forward
			if (targetHits.Length == 0 && Moving)
			{
				MoveTarget.position += MoveDirection;
				RobotAnimator?.SetBool("Walk Forward", true);
			}
			else
			{
				Moving = false;
			}

			// Always look toward the move direction
			transform.forward = MoveDirection;
		}

		// Handle walking animation
		if (!Moving) RobotAnimator?.SetBool("Walk Forward", false);
	}
}
