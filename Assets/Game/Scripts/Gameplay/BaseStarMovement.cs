using UnityEngine;
using System.Collections;

public class BaseStarMovement : MonoBehaviour 
{
	[SerializeField] protected float speed = 2.0f;

	protected StarStats stats;

	protected virtual void Awake()
	{
		stats = GetComponent<StarStats>();
	}

	protected virtual void Move(Vector2 movement)
	{
		transform.Translate(movement * speed * Time.deltaTime);
	}
}
