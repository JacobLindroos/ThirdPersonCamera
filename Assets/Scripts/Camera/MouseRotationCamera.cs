using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationCamera : MonoBehaviour
{
	public float cameraSensitivityX = 1f;
	public float cameraSensitivityY = 0.5f;
	public float cameraZoomValue = 0.4F;
	public float yAngleMin = 10f;
	public float yAngleMax = 50f;
	public float cameraOffsetMin = 0.2f;
	public float cameraOffsetMax = 10f;

	public GameObject target;
	public float cameraOffset = 10f;

	private float currentY = 0f;
	private float currentX = 0f;

	// Start is called before the first frame update
	void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
		//Zoom in and out using mouse scrollwheel
		#region Zoom function
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			cameraOffset += cameraZoomValue;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			cameraOffset -= cameraZoomValue;
		}
		#endregion

	}

	private void LateUpdate()
	{
		currentY += Input.GetAxis("Mouse Y") * cameraSensitivityY;
		currentX = Input.GetAxis("Mouse X") * cameraSensitivityX;

		currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
		cameraOffset = Mathf.Clamp(cameraOffset, cameraOffsetMin, cameraOffsetMax);

		target.transform.Rotate(0, currentX, 0);

		Vector3 direction = new Vector3(0, 0, -cameraOffset);
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(currentY, desiredAngle, 0);
		transform.position = target.transform.position + (rotation * direction);

		transform.LookAt(target.transform);
	}
}
