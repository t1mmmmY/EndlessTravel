using UnityEngine;
using System.Collections;

public class MySexyRigidbody : MonoBehaviour 
{
	private float mass = 1.0f;
	public Vector3 velocity = Vector3.zero;
	private float slowDown = 0.95f;

	void Update()
	{
		transform.Translate(velocity * Time.deltaTime, Space.World);
		velocity *= slowDown;
	}

	public void AddForce(Vector3 force)
	{
		velocity += force;
	}
}
