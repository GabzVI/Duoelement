using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] Transform Target;
	private Camera mainCamera;
	Vector3 camPos;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main;
		camPos.x = Target.transform.position.x;
		camPos.y = Target.transform.position.y;
		transform.position = camPos;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		camPos.y = Mathf.Clamp(camPos.y, -0.38f, 20f);
		camPos.z = -10;


		transform.position = new Vector3(Target.position.x, camPos.y, camPos.z);
	}
}
