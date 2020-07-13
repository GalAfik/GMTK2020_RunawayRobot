using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public Transform ObjectToFollow;
	public Vector3 CameraOffset;

	[Range(0.01f, 1.0f)]
	public float SmoothFactor = 0.01f;

    // Update is called once per frame
    void LateUpdate()
    {
		Vector3 newPosition = ObjectToFollow.position + CameraOffset;
		transform.position = Vector3.Slerp(transform.position, newPosition, SmoothFactor);
    }
}
