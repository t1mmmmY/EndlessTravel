using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] Transform target;
	[SerializeField] Vector3 shift;
	[SerializeField] float speed = 10.0f;

	void OnEnable()
	{
		StarsManager.onGameOver += OnGameOver;
	}

	void OnDisable()
	{
		StarsManager.onGameOver -= OnGameOver;
	}

	void Update()
	{
		if (target != null)
		{
			transform.LookAt(target);
			transform.position = Vector3.Lerp(transform.position, target.position + shift, Time.deltaTime * speed);
		}
	}

	void OnGameOver()
	{
		Debug.Log("GAME OVER");
	}
}
