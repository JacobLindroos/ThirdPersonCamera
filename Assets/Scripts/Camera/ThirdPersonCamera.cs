using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	public float damping = 1F;

	public GameObject target;
	public Vector3 cameraOffset;


    // Start is called before the first frame update
    void Start()
    {
		//calculates the cameras offset position relative to the target
		cameraOffset = transform.position - target.transform.position;
    }

	private void LateUpdate()
	{
		//updates the cameras offset position while running
		Vector3 desiredOffsetPosition = target.transform.position + cameraOffset;
		//giving a smoother movement when following target
		Vector3 currentPosition = Vector3.Lerp(transform.position, desiredOffsetPosition, Time.deltaTime * damping);
		transform.position = currentPosition;

		transform.LookAt(target.transform.position);
	}
}
