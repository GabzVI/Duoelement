using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] CharacterController2d controller;
	[SerializeField] Animator animator;
	public float runSpeed = 2f;

	float horizontalMove = 0f;
	bool jump = false;

	// Update is called once per frame
	void Update()
	{
		//checks if user has pressed left or right
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("forwardSpeed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
		if (Input.GetButtonUp("Jump"))
		{
			jump = false;
		}

	}

	private void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
	}
}
