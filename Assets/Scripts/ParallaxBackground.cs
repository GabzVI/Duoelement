using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

	[SerializeField] Transform cameraTransform;
	[SerializeField] Vector2 parallaxEffectMultiplier;
	private Vector3 lastCampos;
	private float textureUnitSizeX;
	private float textureUnitSizeY;
	private void Start()
	{
		cameraTransform = Camera.main.transform;
		lastCampos = cameraTransform.position;
		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
		textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		//calculates the amount the camera has moved
		Vector3 deltaMovement = cameraTransform.position - lastCampos;
		transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
		lastCampos = cameraTransform.position;

		//Checks if the camera has passed the sprite max x value so we can shift the sprite back in place
		if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
		{
			float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
			transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
		}
		/*if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
		{
			float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
			transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y + offsetPositionY);
		}*/
	}
}
