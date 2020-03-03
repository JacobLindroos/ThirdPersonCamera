using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundCamera : MonoBehaviour
{
	public Transform camTransform;
	public GameObject target;

	public float cameraOffset = 10F;
	public float cameraZoomValue = 0.4F;
	public float sensitivityX = 4.0f;
	public float sensitivityY = 1.0f;
	public float yAngleMin = 0F;
	public float yAngleMax = 75F;
	public float cameraOffsetMax = 10F;
	public float cameraOffsetMin = 0.2F;

	private Camera camera;

	private float trandis;
	private float currentX = 0F;
	private float currentY = 0F;
	private const float TransMin = 1F;
	private const float TransMax = 2F;

	private void Start()
	{
		camTransform = transform;
		camera = Camera.main;
	}

	private void Update()
	{
		currentX += Input.GetAxis("Mouse X") * sensitivityX;
		currentY += Input.GetAxis("Mouse Y") * sensitivityY;

		currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
		cameraOffset = Mathf.Clamp(cameraOffset, cameraOffsetMin, cameraOffsetMax);
		trandis = Mathf.Clamp(trandis, TransMin, TransMax) - 1F;

		target.GetComponent<Renderer>().material.color = new Color(target.GetComponent<Renderer>().material.color.r, target.GetComponent<Renderer>().material.color.g, target.GetComponent<Renderer>().material.color.b, trandis);

		//Sets target mesh to transparency when camera is inside the target
		#region Changes targets renderer
		if (cameraOffset <= 1)
		{
			target.GetComponent<Renderer>().enabled = false;
		}
		if(cameraOffset > 1)
		{
			target.GetComponent<Renderer>().enabled = true;
		}
		#endregion

		//Zoom in and out using mouse scrollwheel
		#region Zoom function
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			cameraOffset += cameraZoomValue;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			cameraOffset -= cameraZoomValue;
		}
		#endregion
	}

	private void LateUpdate()
	{
		Vector3 direction = new Vector3(0, 0, -cameraOffset);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = target.transform.position + rotation * direction;

		target.transform.Rotate(Vector3.up * currentX);

		camTransform.LookAt(target.transform.position);
	}
}
