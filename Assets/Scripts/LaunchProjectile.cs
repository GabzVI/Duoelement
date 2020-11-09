using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Element.Combat
{
	public class LaunchProjectile : MonoBehaviour
	{
		[SerializeField] GameObject prefabBoomerang;
		[SerializeField] float maxLifeTimer = 3f;
		[SerializeField] GameObject boomerangSpawner;
	
		GameObject boomerangs = null;
		Vector3 playerPos;
		public int boomerangCounter;

		// Update is called once per frame
		void Update()
		{
			while(boomerangCounter < 5)
			{
				if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					ThrowBomerang();
				}
			}
			playerPos = gameObject.transform.position;
		}
		private void ThrowBomerang()
		{
			boomerangs = Instantiate(prefabBoomerang, boomerangSpawner.transform.position, Quaternion.identity);
			boomerangs.GetComponent<BoomerangProjectile>().lastboomerangSpawnerPos = boomerangSpawner.transform.position;
			boomerangs.GetComponent<BoomerangProjectile>().lastPlayerPos = playerPos;
			boomerangCounter++;
		}

	}
}

