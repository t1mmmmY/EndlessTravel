using UnityEngine;
using System.Collections;

public class MySexyRigidbody : MonoBehaviour 
{
	public float mass = 1.0f;
	public Vector3 velocity = Vector3.zero;
	public float slowDown = 0.95f;

	void Update()
	{
		transform.Translate(velocity, Space.World);
		
		velocity *= slowDown;
	}

	public void AddForce(Vector3 force)
	{
		velocity += force;
	}
}
