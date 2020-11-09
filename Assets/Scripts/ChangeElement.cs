using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeElement : MonoBehaviour
{
	[SerializeField] AnimatorOverrideController earthRunOverride;
	[SerializeField] AnimatorOverrideController waterRunOverride;
	[SerializeField] AnimatorOverrideController airRunOverride;



	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{ 
			ChangeRunToEarth();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			ChangeRunToWater();
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			ChangeRunToAir();
		}
	}

	public void ChangeRunToEarth()
	{
		GetComponent<Animator>().runtimeAnimatorController = earthRunOverride as RuntimeAnimatorController;
	}
	public void ChangeRunToWater()
	{
		GetComponent<Animator>().runtimeAnimatorController = waterRunOverride as RuntimeAnimatorController;
	}
	public void ChangeRunToAir()
	{
		GetComponent<Animator>().runtimeAnimatorController = airRunOverride as RuntimeAnimatorController;
	}
	

}
