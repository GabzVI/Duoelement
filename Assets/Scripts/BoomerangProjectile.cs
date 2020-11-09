using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Element.Combat
{
	public class BoomerangProjectile : MonoBehaviour
	{
		[SerializeField] float boomerangSpeed = 10f;
		[SerializeField] float boomerangAccelerator = 7f;
		[SerializeField] float spawnTimer = 0f;
		GameObject player;

		float distanceBoomerangToPlayer;
		float distanceBoomerangToSpawner;
		private float maxDistanceFromSpawner = 5f;
		private float maxDistanceFromPlayer = 3f;
		public Vector3 lastboomerangSpawnerPos;
		public Vector3 lastPlayerPos;

		public void Start()
		{
			player = GameObject.FindWithTag("Player");
		}
		public void MoveBoomerang()
		{
			distanceBoomerangToSpawner = Vector2.Distance(lastboomerangSpawnerPos, gameObject.transform.position);
			distanceBoomerangToPlayer = Vector2.Distance(lastPlayerPos, gameObject.transform.position);

			if (distanceBoomerangToSpawner < maxDistanceFromSpawner)
			{
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(boomerangSpeed * boomerangAccelerator, 0));
			}
			else if (distanceBoomerangToPlayer > 1)
			{
				gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-boomerangSpeed * boomerangAccelerator, 0));
			}
			
		}

		private void Update()
		{
			SpawnTimeCounter();

			MoveBoomerang();
			if(spawnTimer > 3)
			{
				player.GetComponent<LaunchProjectile>().boomerangCounter--;
				Destroy(gameObject);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				player.GetComponent<LaunchProjectile>().boomerangCounter--;
				Destroy(gameObject);
			}
		}

		public void SpawnTimeCounter()
		{
			spawnTimer += Time.deltaTime;
		}

		public void ResetCounter()
		{
			spawnTimer = 0f;
		}
		
	}
}

