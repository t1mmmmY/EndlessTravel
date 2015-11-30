using UnityEngine;
using System.Collections;
using CnControls;

public class StarController : MonoBehaviour 
{
	[SerializeField] float speed = 2.0f;


	void Update()
	{
		Vector2 movement = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));

		if (movement != Vector2.zero)
		{
			Move(movement);
		}
	}

	void Move(Vector2 movement)
	{
		transform.Translate(movement * speed * Time.deltaTime);
	}
}
