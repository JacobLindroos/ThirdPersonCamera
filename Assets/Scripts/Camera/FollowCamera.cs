using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public float damping = 1F;

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
		//calculates the cameras rotation, using it´s angle towards the target  
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		
		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		//updates the cameras position minus it´s offset to the target
		transform.position = target.transform.position - (rotation * cameraOffset);
		//looks at target
		transform.LookAt(target.transform);
	}
}
