using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2d : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                           // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;   // A collider that will be disabled when crouching
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Transform lookAheadTarget;
	[SerializeField] float aheadAmount, aheadSpeed;
	[SerializeField] ParticleSystem footSteps;
	private ParticleSystem.EmissionModule footEmission;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D playerRB;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Awake()
	{
		playerRB = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		footEmission = footSteps.emission;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, playerRB.velocity.y);
			// And then smoothing it out and applying it to the character
			playerRB.velocity = Vector3.SmoothDamp(playerRB.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
				spriteRenderer.flipX = false;
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

			//Move camera point
			if(Input.GetAxisRaw("Horizontal") != 0)
			{
				lookAheadTarget.localPosition = new Vector3(Mathf.Lerp(lookAheadTarget.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), lookAheadTarget.localPosition.y, lookAheadTarget.localPosition.z);
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			playerRB.AddForce(new Vector2(0f, m_JumpForce));
		}

		if(!jump && playerRB.velocity.y > 0)
		{
			playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f * Time.deltaTime);
		}

		//Show Footsteps Effect
		if (Input.GetAxisRaw("Horizontal") != 0 && m_Grounded)
		{
			footEmission.rateOverTime = 25f;
		}
		else
		{
			footEmission.rateOverTime = 0f;
		}


	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		spriteRenderer.flipX = true;
	}
}
