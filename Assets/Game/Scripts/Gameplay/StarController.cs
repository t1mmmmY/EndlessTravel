using UnityEngine;
using System.Collections;
using CnControls;

public class StarController : BaseStarMovement 
{
	void Update()
	{
		Vector2 movement = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));

		if (movement != Vector2.zero)
		{
			Move(movement);
		}
	}

}
