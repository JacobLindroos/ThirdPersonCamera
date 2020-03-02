using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationCamera : MonoBehaviour
{
	public float rotateSpeed = 5F;

	public GameObject target;
	Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
		cameraOffset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void LateUpdate()
	{
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * cameraOffset);

		transform.LookAt(target.transform);
	}
}
