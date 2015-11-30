using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] Camera camera;
	[SerializeField] Transform target;
	[SerializeField] Vector3 shift;
	[SerializeField] float speed = 10.0f;
	[SerializeField] float standartVerticalShift = 1;

	StarStats playableStar;


	void Start()
	{
		standartVerticalShift = shift.z;
	}

	void OnEnable()
	{
		StarsManager.onGameOver += OnGameOver;
		StarsManager.onSetPlayableStar += OnSetPlayableStar;
	}

	void OnDisable()
	{
		StarsManager.onGameOver -= OnGameOver;
		StarsManager.onSetPlayableStar -= OnSetPlayableStar;
	}

	void Update()
	{
		if (target != null)
		{
			shift.z = standartVerticalShift + 1 - playableStar.power;
			transform.LookAt(target);
			transform.position = Vector3.Lerp(transform.position, target.position + shift, Time.deltaTime * speed);
		}
	}

	void OnGameOver()
	{
//		Debug.Log("GAME OVER");
	}

	void OnSetPlayableStar(StarStats stats)
	{
		playableStar = stats;
		target = stats.transform;
	}
}
